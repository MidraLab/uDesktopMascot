import os
import sys
import pandas as pd
import openai
from git import Repo
from io import StringIO

# OpenAI APIキーの設定
openai.api_key = os.getenv('OPENAI_API_KEY')
if not openai.api_key:
    print("Error: OpenAI API key is not set.")
    sys.exit(1)

# CSVファイルのパス
csv_path = 'Assets/uDesktopMascot/LocalizationTable/LocalizationTable.csv'

# 現在のCSVを読み込み
after_df = pd.read_csv(csv_path)

# 対象の言語とその表示名を設定
target_languages = {
    'English(en)': '英語',
    'French(fr)': 'フランス語',
    'Italian(it)': 'イタリア語',
    'Korean(ko)': '韓国語'
}

# Gitリポジトリを初期化
repo = Repo('.')

# ベースブランチ（通常は'main'または'master'）
base_branch = os.getenv('GITHUB_BASE_REF', 'main')

# ベースブランチをフェッチ
origin = repo.remotes.origin
origin.fetch()

# ベースブランチのCSVを取得
try:
    base_csv_content = repo.git.show(f'origin/{base_branch}:{csv_path}')
    before_df = pd.read_csv(StringIO(base_csv_content))
except Exception as e:
    # ベースブランチにファイルが存在しない場合（新規追加など）
    before_df = pd.DataFrame(columns=after_df.columns)

# データフレームをマージして更新または新規エントリを特定
merged_df = after_df.merge(before_df, on='Key', how='left', suffixes=('', '_before'))

# 日本語のテキストが新規追加または更新された行を特定
updated_rows = merged_df[
    (merged_df['Japanese(ja)'] != merged_df['Japanese(ja)_before']) |
    (merged_df['Japanese(ja)_before'].isnull() & merged_df['Japanese(ja)'].notnull())
]

# 更新がない場合はスクリプトを終了
if updated_rows.empty:
    print("新規または更新された日本語テキストはありません。")
    sys.exit(0)

# 各更新行に対して翻訳を実行
for _, row in updated_rows.iterrows():
    source_text = row['Japanese(ja)']
    key = row['Key']
    print(f"キー '{key}' の翻訳を実行します。")
    for column, language_name in target_languages.items():
        current_translation = row.get(column, '')
        # 翻訳が未実行または日本語テキストが更新された場合のみ翻訳を実行
        if pd.isnull(current_translation) or current_translation == '':
            prompt = f"以下の日本語テキストを{language_name}に翻訳してください：\n\n{source_text}"
            try:
                response = openai.Completion.create(
                    engine="text-davinci-003",
                    prompt=prompt,
                    max_tokens=1000,
                    n=1,
                    stop=None,
                    temperature=0.3,
                )
                translation = response.choices[0].text.strip()
                after_df.loc[after_df['Key'] == key, column] = translation
                print(f"{language_name}への翻訳結果：{translation}")
            except Exception as e:
                print(f"キー '{key}' の {language_name} への翻訳中にエラーが発生しました：{e}")

# 更新されたCSVを保存
after_df.to_csv(csv_path, index=False)
print("翻訳が完了し、CSVファイルが更新されました。")