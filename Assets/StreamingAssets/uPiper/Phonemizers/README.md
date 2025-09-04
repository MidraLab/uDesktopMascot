# Phonemizer Data Files

This directory contains data files required for phonemization.

## CMU Pronouncing Dictionary

The CMU Pronouncing Dictionary (cmudict) is required for the RuleBasedPhonemizer to function.

### Download Instructions

1. Download the CMU dictionary from:
   - http://www.speech.cs.cmu.edu/cgi-bin/cmudict
   - Or directly: https://raw.githubusercontent.com/cmusphinx/cmudict/master/cmudict.dict

2. Save the file as `cmudict-0.7b.txt` in this directory

3. The file should be approximately 3-4 MB in size

### License

The CMU Pronouncing Dictionary is released into the public domain.

### Format

The dictionary contains entries in the format:
```
WORD  W ER1 D
HELLO  HH AH0 L OW1
WORLD  W ER1 L D
```

Where:
- The first field is the word in uppercase
- Following fields are ARPABET phonemes
- Numbers indicate stress (0=no stress, 1=primary, 2=secondary)

## Additional Language Data

For other languages, place the corresponding dictionary files here:
- French: `fr-dict.txt`
- German: `de-dict.txt`
- Spanish: `es-dict.txt`

## Data Structure

```
StreamingAssets/
└── uPiper/
    └── Phonemizers/
        ├── README.md (this file)
        ├── cmudict-0.7b.txt
        └── [other language files]
```