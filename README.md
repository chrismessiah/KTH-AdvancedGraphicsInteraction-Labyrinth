# Labyrinth

### Remove contaminating git history

First install [bfg](https://rtyley.github.io/bfg-repo-cleaner/). If on macOS, you can install it using [homebrew](https://brew.sh/)

```
$ bfg --delete-files *.mat.meta
$ bfg --delete-files *.mat
$ bfg --delete-files *.meta
$ git reflog expire --expire=now --all && git gc --prune=now --aggressive
```