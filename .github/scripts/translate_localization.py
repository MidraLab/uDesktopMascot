import os
import sys
import pandas as pd
from openai import OpenAI
import csv

client = OpenAI(api_key=os.environ["OPENAI_API_KEY"])

# CSVファイルのパス
csv_path = 'Assets/uDesktopMascot/LocalizationTable/LocalizationTable.csv'

# 現在のCSVを読み込み
df = pd.read_csv(csv_path)

# 対象の言語とその表示名を設定（言語名は英語に変更）
target_languages = {
    'English(en)': 'English',
    'French(fr)': 'French',
    'Italian(it)': 'Italian',
    'Korean(ko)': 'Korean'
}

# 翻訳用の関数を定義
def translate_text(text, target_language):
    try:
        response = client.chat.completions.create(
            model="gpt-4o-mini",
            messages=[
                {"role": "system", "content": "You are a helpful assistant."},
                {"role": "user", "content": f"Translate the following Japanese text to {target_language}. Return only the translated text without any additional explanations or notes.\n\n{text}"}
            ]
        )
        # 翻訳結果を取得
        translation = response.choices[0].message.content.strip()
        return translation
    except Exception as e:
        print(f"翻訳中にエラーが発生しました：{e}")
        return None

# 各行に対して翻訳を実行
for idx, row in df.iterrows():
    japanese_text = row.get('Japanese(ja)', '')
    key = row.get('Key', '')
    
    # 日本語テキストが存在する場合のみ翻訳を実行
    if pd.notnull(japanese_text) and japanese_text.strip() != '':
        print(f"キー '{key}' の翻訳を実行します。")
        for column, target_language in target_languages.items():
            current_translation = row.get(column, '')
            # 翻訳列が存在しない場合、列を追加
            if column not in df.columns:
                df[column] = ''
            # 翻訳が未実行または空欄の場合のみ翻訳を実行
            if pd.isnull(current_translation) or current_translation.strip() == '':
                translation = translate_text(japanese_text, target_language)
                if translation:
                    df.at[idx, column] = f'"{translation}"'
                    print(f"{target_language}への翻訳結果：{translation}")
                else:
                    print(f"キー '{key}' の {target_language} への翻訳に失敗しました。")
    else:
        print(f"キー '{key}' に日本語テキストが存在しないため、翻訳をスキップします。")

# 更新されたCSVを保存
df.to_csv(csv_path, index=False, encoding='utf-8', quoting=csv.QUOTE_ALL)
print("翻訳が完了し、CSVファイルが更新されました。")