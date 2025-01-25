import os
import sys
import pandas as pd
import openai

# OpenAI APIキーの設定
openai.api_key = os.getenv('OPENAI_API_KEY')
if not openai.api_key:
    print("Error: OpenAI API key is not set.")
    sys.exit(1)

# CSVファイルのパス
csv_path = 'Assets/uDesktopMascot/LocalizationTable/LocalizationTable.csv'

# 現在のCSVを読み込み
df = pd.read_csv(csv_path)

# 対象の言語とその表示名を設定
target_languages = {
    'English(en)': '英語',
    'French(fr)': 'フランス語',
    'Italian(it)': 'イタリア語',
    'Korean(ko)': '韓国語'
}

# 各行に対して翻訳を実行
for idx, row in df.iterrows():
    japanese_text = row.get('Japanese(ja)', '')
    key = row.get('Key', '')
    
    # 日本語テキストが存在する場合のみ翻訳を実行
    if pd.notnull(japanese_text) and japanese_text != '':
        print(f"キー '{key}' の翻訳を実行します。")
        for column, language_name in target_languages.items():
            current_translation = row.get(column, '')
            # 翻訳列が存在しない場合、列を追加
            if column not in df.columns:
                df[column] = ''
            # 翻訳が未実行または空欄の場合のみ翻訳を実行
            if pd.isnull(current_translation) or current_translation == '':
                prompt = f"以下の日本語テキストを{language_name}に翻訳してください：\n\n{japanese_text}"
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
                    df.at[idx, column] = translation
                    print(f"{language_name}への翻訳結果：{translation}")
                except Exception as e:
                    print(f"キー '{key}' の {language_name} への翻訳中にエラーが発生しました：{e}")
    else:
        print(f"キー '{key}' に日本語テキストが存在しないため、翻訳をスキップします。")

# 更新されたCSVを保存
df.to_csv(csv_path, index=False)
print("翻訳が完了し、CSVファイルが更新されました。")