
# Pack-My-Game

Téléchargez [PackMyGame-x86.zip](https://github.com/daerlnaxe/Pack-My-Game/blob/master/PackMyGame%20x86%20-%20A03.zip) ou [PackMyGame-x64.zip](https://github.com/daerlnaxe/Pack-My-Game/blob/master/PackMyGame%20x64%20-%20A02.zip) pour les fichiers executables.

## A quoi ça sert:
 * Copie et compresse tout d'un jeu contenu dans la base de l'application LaunchBox.
 * Génère un court fichier xml avec les informations principales sur le jeu.
 * Copie les images, les manuels, video, music, fichiers roms.
 * Copie aussi les fichiers de code de triche si vous spécifiez le chemin (le nom doit être ainsi 'GameName-*.*')
 * Génère un fichier montrant l'arborescence
 * 7z et zip compression
	
## Pourquoi ?
 * En tant que joueur Français je voulais sauver toutes mes modifications et les conserver pour plus tard, au cas où.

## Note
 * Ne déplace ni n'efface JAMAIS aucune fichier source.
 * Demande avant d'écraser les roms, manuels, musiques, vidéos qui sont déjà dans le dossier de TRAVAIL au cours de la copie
 * Actuellement le programme demande une permission globale avant d'écraser les fichiers images en cas d'éventuel conflit qu'il y ait ou non des fichiers images dans le répertoire de travail.
 * Les clones ajoutés ne le sont qu'à condition d'être groupés dans LaunchBox
		
## Versions

### Alpha10: 27/08/2018
* Mise en place d'un système d'upgrade du fichier configuration pour suivre les builds.
* Traduction française terminée.
* Fichiers de traduction des variables dispo.
* Quelques modifications à propos de la configuration (verification des chemins, possibilités de copier/coller...)
* Bug fixes en section configuration
* Integration des sections d'aide et des crédits.

### Alpha02: 26/08/2018
 * Box to ask for images 
 * Externalization of method: transfers verification (possibility to evolve to "decision for all" system, silent debug...)
 * Boucle pour lancer plusieurs jeux (implémenté mais pas réellement testé - le mode jeu seul fonctionne sur la même méthode)
 * Lever de la listview le jeu packé
 
### Alpha01: 08/2018
 * 1st functionnal release
 * Module pour trad: Config 50%
 * DotNetZip implanté à la place de FileZip		
 * Sauve le log
 * Fenetre log mode autokill, keep: ok
 * Boxe maison de gestion des collisions
 * Méthode pour rajouter le dossier cheatcode
 * Choix 7z 
 * Création d'un Schéma du contenu
 * Ajout des clones

		
### TODO
 - [ ] Filtrer les plateformes
 - [ ] Securité sur le dossier de travail sur la page principale (en plus de celle de PackMe)
 - [ ] Trouver un meilleur moyen de gérer la confirmation au niveau des images
 - [ ] Corriger la version anglaise
 - [ ] Ajouter miniature pour les photos sur la box de collision (voir si nécessaire)
 - [ ] Ajout d'un mode debug silencieux
 - [ ] Mode silent sans box ? (All overwrite)
 - [ ] Mode silent sans fenêtre window
 - [ ] Fenetre contextuelle clic droit list view, avec justZip & just7Z + faire infoxml seul, tree seul.
 - [ ] Editer les infos dans la short list ? => signifie de charger la totalité du jeu
 - [ ] Splashscreen au loading
 - [ ] Ameliorate config with own browser system  and box path editable (personel: voir dans Gesum, probablement déjà présent)
 - [ ] md5 Compareason md5 (Upgrader le projet dlnxLocalTransfert) ?
 - [ ] Bouger VFolder, HFolder, copyfile, reconstruct path (mettre une option basique de sécurité sur le chemin)
