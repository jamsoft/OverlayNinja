![Ninja logo](Ninja-Toy-icon.png?raw=true)
# OverlayNinja
A little tool to help defeat the shell icon overlay space wars being wraged on our development machines

![Ninja logo](screenshot.png?raw=true)

# Eh?  What?

Amazingly even Windows 10 still only supports upto 15 shell icon overlays.  Any number over this just gets ignored, so apps fight to keep theirs at the top.

As a dev (and you probably are too if you're reading this) I've gotten utterly fed up with various tools hijacking and forcing their way to the top of the shell overlay icon list.

I've got tools like Svn, Git, Dropbox, One Drive and many others installed and they fight to get to the top.  Dropbox is particularly bad.

I got so annoyed today that I wrote this little app to help, rather than constantly manually editing the registry we can launch this and do the job in a couple of clicks.

# x86 / x64

You'll need to run this as admin and pick the right version for your OS architecture.  I've added in a requires administrator level access into the app.manifest so your machine should elevate.

# Problems

As ever, submit issues or pull requests if you improve it.  I've tested this as far as it works great on my Windows 10 box but we are dealing with the registry so worth making a *.reg backup of the relevant keys before using.

HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\ShellIconOverlayIdentifiers
HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Explorer\ShellIconOverlayIdentifiers

USE AT YOUR OWN RISK, I ACCEPT NO RESPONSIBILITY FOR ANY DAMAGE CAUSED!

"Works on my machine" :)