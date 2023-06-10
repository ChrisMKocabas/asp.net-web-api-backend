# 1 - warning: CRLF will be replaced by LF in some-file The file will have its original line endings in your working directory.

### Warning, your local changes will be lost, so, commit FIRST

So, i was getting this error in git

warning: CRLF will be replaced by LF in some-file The file will have its original line endings in your working directory.

All you have to do is to follow the steps in the git docs:

$ git config --global core.autocrlf input # Configure Git on OS X to properly handle line endings
$ git rm --cached -r . && git reset --hard # Warning, your local changes will be lost, so commit FIRST

And everyone live happy ever after.
