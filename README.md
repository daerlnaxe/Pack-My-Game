# Pack-My-Game

Download [PackMyGame-x86.zip](https://github.com/daerlnaxe/Pack-My-Game/blob/master/PackMyGame%20x86%20-%20A03.zip) or [PackMyGame-x64.zip](https://github.com/daerlnaxe/Pack-My-Game/blob/master/PackMyGame%20x64%20-%20A02.zip) for executable files  

## What it do:
 * It copies then compress everything about a game contained in the db of LaunchBox app.
 * It generates a short xml file with the main information about the game
 * It takes images, pdf manual, video, music, rom file.
 * It takes also the cheatcode files if you fill the path ('GameName-*.*')
 * It generates a tree view file.
 * 7z and zip compression
	
## Why ?
 * Because as a french gamer i wanted to save everything i fill about my games and keep it
for later, just in case there was a problem. 

## Note
 * Use it only with roms.
 * Don't move or delete files from the source (never)
 * It asks before to overwrite roms, manuals, music, video that are in the working directory meanwhile
 the copy (the target directory)
 * Currently it asks a global permission to overwrite for the image/pictures files, even if there is
no image file in the the destination folder.
 * The clones are added only if they are grouped with a main in LaunchBox
 * Compression 7z and zip
 * It logs everything during the game treatment in a window, and a file.
		
## Version

### Alpha10: 27/08/2018
 * Set up of a system updating parameters to follow builds upgrade.
 * French Translation achieved
 * Translation files for variables
 * Some modifications about configuration (path verification, possibility to copy/paste...)
 * Bug fixes in configuration section
 * Integration of the help and credits sections 

### Alpha02: 26/08/2018
 * Box to ask for images 
 * Externalization of method: transfers verification (possibility to evolve to "decision for all" system
silent debug...)
 * Loop to process on  many games (implemented but not really tested - one game works with the same method)
 * Remove from listview the game packed

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


## TODO
- [ ] Filter supports
- [ ] Security on work folder
- [ ] Find a better way to handle images files
- [ ] Correct the english version		
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
