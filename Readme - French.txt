
============ Note
	- Ne déplace ni n'efface aucune fichiers source (jamais)
	- Demande avant d'écraser les roms, manuels, musiques, vidéos qui sont déjà dans le dossier au cours de la copie (dossier cible)
	- Actuellement le programme ne demande pas avant d'écraser les fichiers de type image
	- Les clones ajoutés ne le sont qu'à condition d'être groupés dans LaunchBox
		
============ Version
	# Alpha01: 08/2018
		- 1st functionnal release
		- Module pour trad: Config 50%
		- DotNetZip implanté à la place de FileZip		
		- Sauve le log
		- Fenetre log mode autokill, keep: ok
		- Boxe maison de gestion des collisions
		- Méthode pour rajouter le dossier cheatcode
		- Choix 7z 
		- Création d'un Schéma du contenu
		- Ajout des clones

	# Alpha02: 26/08/2018
		- Box to ask for images 
		- Externalization of method: transfers verification (possibility to evolve to "decision for all" system, silent debug...)
		- Boucle pour lancer plusieurs jeux (implémenté mais pas réellement testé - le mode jeu seul fonctionne sur la même méthode)
		- Lever de la listview le jeu packé
		
============ TODO
				
		- Finir la traduction fr
		- Corriger la version anglaise
		- Intégrer l'aide
		- Ajouter miniatture pour les photos sur la box de collision (voir si nécessaire)
		- Boucle pour procéder sur plusieurs  jeux
		- Ajout d'un mode debug silencieux
		- Mode silent sans box ? (All overwrite)
		- Mode silent sans fenêtre window
		- Fenetre contextuelle clic droit list view, avec justZip & just7Z
		- Editer les infos dans la short list ? => signifie de charger la totalité du jeu
		- Splashscreen au loading
		- Ameliorate config with own browser system  and box path editable (personel: voir dans Gesum, probablement déjà présent)
		- md5 Compareason md5 ?
		- Bouger VFolder, HFolder, copyfile, reconstruct path (mettre une option basique de sécurité sur le chemin)