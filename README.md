# Pack-My-Game

Download PackMyGame x32.zip or PackMyGame x64.zip for executable files  
Téléchargez PackMyGame x32.zip ou PackMyGame x64.zip pour avoir les fichiers exécutables  

Vous pouvez lire une version Française de ce readme dans les fichiers texte.  

## Note
	* Don't move or delete files from the source (never)
	* Ask before to overwrite roms, manuals, music, video that are in the working directory meanwhile
	the copy (the target directory)
	* Currently it ask a global permission to overwrite for the image/pictures files, even if there is
	no image file in the the destination folder.
	* The clones are added only if they are grouped with a main in LaunchBox
	* Compression 7z and zip
		
## Version
### Alpha01: 08/2018
	* 1st functionnal release
	* Trad Module Implantation: Ok
	* DotNetZip implanted instead of FileZip
	* Write Log	
	* Window log mode autokill, keep: ok			
	* Custom Box to handle collisions
	* Method to copy cheatcodes dir
	* 7z choice
	* File Schéma
	* Add Clones
		
### Alpha02: 26/08/2018
	* Box to ask for images 
	* Externalization of method: transfers verification (possibility to evolve to "decision for all" system
	* silent debug...)
	* Loop to process on  many games (implemented but not really tested - one game works with the same method)
	* Remove from listview the game packed

## TODO

- [ ] Find a better way to handle images files
- [ ] Finish Translation fr
- [ ] Correct the english version		
- [ ] Integrate help
- [ ] Carroussel to see image files to overwrite etc... (if necessary)		
- [ ] Add Debug Options in panel config
- [ ] Mode silent without box prompt ? (All overwrite)
- [ ] Mode silent without log window
- [ ] Contextual menu with 7zip only, ziponly ?
- [ ] Edit info in short list ? => it means to load total information of the game.
- [ ] Splashscreen on loading
- [ ] Ameliorate config with own browser system  and box path editable
- [ ] md5 Compareason ?
- [ ] Move VFolder, HFolder, copyfile, reconstruct path (set a security basic path option)
