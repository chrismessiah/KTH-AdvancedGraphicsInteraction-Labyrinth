# Labyrinth

## Prerequisites

Make sure you install it [Blender](https://www.blender.org/) **before** opening the project in Unity the first time. Otherwise you will get import errors!

## Troubleshooting

### Remove contaminating git history

First install [bfg](https://rtyley.github.io/bfg-repo-cleaner/). If on macOS, you can install it using [homebrew](https://brew.sh/)

**MAKE SURE YOU BACKUP BEFORE MAKING CHANGES**

```
$ bfg --delete-files *.mat.meta
$ bfg --delete-files *.mat
$ bfg --delete-files *.meta
$ git reflog expire --expire=now --all && git gc --prune=now --aggressive
```
