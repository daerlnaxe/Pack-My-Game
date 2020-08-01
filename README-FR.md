
# Pack-My-Game

Téléchargez [PackMyGame-x86.zip](https://github.com/daerlnaxe/Pack-My-Game/blob/master/PackMyGame%20x86%20-%20A03.zip) ou [PackMyGame-x64.zip](https://github.com/daerlnaxe/Pack-My-Game/blob/master/PackMyGame%20x64%20-%20A02.zip) pour les fichiers executables.

## A quoi ça sert:
 * Copie et compresse tout d'un jeu contenu dans la base de l'application LaunchBox.
 * Génère un court fichier xml avec les informations principales sur le jeu.
 * Copie les images, les manuels, video, music, fichiers roms.
 * Copie aussi les fichiers de code de triche si vous spécifiez le chemin (le nom doit être ainsi 'GameName-*.*')
 * Génère un fichier montrant l'arborescence
 * 7z et zip compression
 * Sauvegarde les données à propos d'un jeu de LaunchBox xml
 * Quand PackMe run,  crée un fichier xml amélioré, ajoutant les chemins manquants, en plus de faire une sauvegarde des données originelles.
 * Permet de choisir manuellement des fichiers video, music, manuel si la base de données ne mentionne pas les chemins
 * Le nouveau menu contextuel Contextual permet de construire petit à petit le dossier de travail pour le compresser plus tard.
	
## Pourquoi ?
 * En tant que joueur Français je voulais sauver toutes mes modifications et les conserver pour plus tard, au cas où.

## Note
 * Ne déplace ni n'efface JAMAIS aucune fichier source.
 * Demande avant d'écraser les roms, manuels, musiques, vidéos qui sont déjà dans le dossier de TRAVAIL au cours de la copie
 * Actuellement le programme demande une permission globale avant d'écraser les fichiers images en cas d'éventuel conflit qu'il y ait ou non des fichiers images dans le répertoire de travail.
 * Les clones ajoutés ne le sont qu'à condition d'être groupés dans LaunchBox
		
## Versions:
### Beta 1.3.0.7 01/02/2020
 * Correction de plusieurs bugs.
 * Peut packer un jeu dont le titre contient des charactères spéciaux (certains fichiers n'étaient pas pris en compte avant)
 * Fonction permettant de choisir le nom de l'archive compressée
 * Testé sur plus de 50 jeux (rien que sur cette version)
 * Il reste un bug en cas d'abandon, rien de méchant.

### Beta 1.3.0.4: 08/09/2018

 * Visual studio m'a fait un énorme bug sur la fenêtre principale, j'espère avoir tout réparé.
 * Packme: Filtre les clones pour réduire les "prompts".
 * Menu contextuel
 * Plusieurs opérations depuis le menu contextuel (comme zip, 7zip, faire l'info.xml, backup etc...)
 * PackMe: 'Bug' fixé, le fichier xml originel peut ne pas contenir le chemin pour la video, musique, car LaunchBox doit probablement utiliser un algorithme en plus de la base de donnée, maintenant PackMyGame recherchera dans les dossiers dédiés
 et proposera des occurences de fichier. Si c'est déjà checké ça signifie que ça suit le systeme normal de nom. Cochez ou décochez en fonction de vos besoins.
 * Game devient GameInfo, Game est maintenant (je l'espère) la copie de ce que l'on trouve dans le xml de LaunchBox. GameInfo ne contient plus aucun chemin.
 * PackMe: Dans le menu de configuration, un système de checkbox permet de controler le déroulement de PackMe selon vos besoins.
 * PackMe: Crée un fichier contenant les données originelels (OBGame)
 * PackMe: Crée un fichier qui contient les données améliorées, en cas de problèmes (EBGame). ex: en modifiant videopath avec ce que vous avez trouvé manuellement.

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
 - [ ] Work in progress: Eliminate duplicates images files function in contextual menu (md5 calcul)
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
