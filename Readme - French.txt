
============ Note
	- Ne d�place ni n'efface aucune fichiers source (jamais)
	- Demande avant d'�craser les roms, manuels, musiques, vid�os qui sont d�j� dans le dossier au cours de la copie (dossier cible)
	- Actuellement le programme ne demande pas avant d'�craser les fichiers de type image
	- Les clones ajout�s ne le sont qu'� condition d'�tre group�s dans LaunchBox
		
============ Version
	# Alpha01: 08/2018
		- 1st functionnal release
		- Module pour trad: Config 50%
		- DotNetZip implant� � la place de FileZip		
		- Sauve le log
		- Fenetre log mode autokill, keep: ok
		- Boxe maison de gestion des collisions
		- M�thode pour rajouter le dossier cheatcode
		- Choix 7z 
		- Cr�ation d'un Sch�ma du contenu
		- Ajout des clones

	# Alpha02: 26/08/2018
		- Box to ask for images 
		- Externalization of method: transfers verification (possibility to evolve to "decision for all" system, silent debug...)
		- Boucle pour lancer plusieurs jeux (impl�ment� mais pas r�ellement test� - le mode jeu seul fonctionne sur la m�me m�thode)
		- Lever de la listview le jeu pack�
		
============ TODO
				
		- Finir la traduction fr
		- Corriger la version anglaise
		- Int�grer l'aide
		- Ajouter miniatture pour les photos sur la box de collision (voir si n�cessaire)
		- Boucle pour proc�der sur plusieurs  jeux
		- Ajout d'un mode debug silencieux
		- Mode silent sans box ? (All overwrite)
		- Mode silent sans fen�tre window
		- Fenetre contextuelle clic droit list view, avec justZip & just7Z
		- Editer les infos dans la short list ? => signifie de charger la totalit� du jeu
		- Splashscreen au loading
		- Ameliorate config with own browser system  and box path editable (personel: voir dans Gesum, probablement d�j� pr�sent)
		- md5 Compareason md5 ?
		- Bouger VFolder, HFolder, copyfile, reconstruct path (mettre une option basique de s�curit� sur le chemin)