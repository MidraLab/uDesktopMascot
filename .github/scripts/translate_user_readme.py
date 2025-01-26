from openai import OpenAI
import os

client = OpenAI(api_key=os.environ["OPENAI_API_KEY"])

def translate_text(text, target_language):
    completion = client.chat.completions.create(
        model="gpt-4o-mini",
        messages=[
            {"role": "system", "content": "You are a helpful assistant."},
            {"role": "user", "content": f"Translate the following text to {target_language}: {text}"}
        ]
    )
    # メッセージのコンテンツに直接アクセスする
    return completion.choices[0].message.content

def main():
    # Path to the original README
    original_readme_path = 'Assets/uDesktopMascot/Document/README.txt'

    # Read the original README
    with open(original_readme_path, 'r', encoding='utf-8') as file:
        original_text = file.read()

    # Define the target languages and their corresponding file suffixes
    languages = {
        'English': 'EN',
        'Chinese': 'CN',
        'Spanish': 'ES',
        'French': 'FR',
    }

    for language, suffix in languages.items():
        print(f"Translating to {language}...")

        # Translate the text
        translated_text = translate_text(original_text, language)

        # Write the translated text to the corresponding file
        output_path = f'Assets/uDesktopMascot/Document/README_{suffix}.txt'
        with open(output_path, 'w', encoding='utf-8') as file:
            file.write(translated_text)

        print(f"Translation to {language} completed.")

if __name__ == "__main__":
    main()