
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
 * (Retiré)Le nouveau menu contextuel Contextual permet de construire petit à petit le dossier de travail pour le compresser plus tard.
 * Unpack permet de réinjecter dans LaunchBox
 * Calcul d'un fichier somme à la fin de la compression
 * Extraction des données concernant la plateforme
 * Les données extraites (TBGame & TBPlatform) sont certifiées verbatim, les données sont directement extraites des balises sans passer par une classe container.
 * Toute copie de fichier est vérifiée, vous pouvez le voir dans le traçage
 
	
	
## Pourquoi ?
 * En tant que joueur Français je voulais sauver toutes mes modifications et les conserver pour plus tard, au cas où.

## Note
 * Ne déplace ni n'efface JAMAIS aucune fichier source.
 * Unpack fait une backup du fichier de la plateforme avant de modifier.
 * Demande avant d'écraser les roms, manuels, musiques, vidéos qui sont déjà dans le dossier de TRAVAIL au cours de la copie 
 * Actuellement le programme demande une permission globale avant d'écraser les fichiers images en cas d'éventuel conflit qu'il y ait ou non des fichiers images dans le répertoire de travail.
 * Les clones ajoutés ne le sont qu'à condition d'être groupés dans LaunchBox
		
## Versions:

### Alpha 2.0.0.0
 * Gros changements, l'application migre au système wpf plutôt que forms.
 * .Net Core + .Net standard pour les librairies externes
 * Unpack-My-Game terminé, pour réinjecter vos jeux dans LaunchBox (plusieurs modes)
 * Calcule des fichiers md5 après la compression (permet de vérifier l'intégrité pour plus tard)
 * extracte les données d'une plateforme pour conserver ça pour plus tard (mode à venir pour l'utiliser dans unpack-my-game)
 * Les données extraitent nommées 'True Backup' n'utilisent pas de class, pour suivre l'évolution de LaunchBox sans gestion. S'il n'y a pas de gros changement à propos des fichiers xml ça devrait suivre les updates sans modification nécessaire.
  * Grosses modifications au sujet du comportement des fenêtres demandant quoi faire  en cas de conflit de fichiers (séparation entre les fichiers images, du reste des fichiers au niveau de la mémorisation des choix) 
 * Quelques bugs mineurs sont connus, référez vous à la section avant d'utiliser.
 * Sépare une grosse partie du travail sur les fichiers xml afin que ça soit réutilisable (vous pouvez vous en servir mais il y a du nettoyage à faire)
 * Nouveau système de traduction pour Pack-My-Game (utilise des fichiers xml, vous avez juste à reprendre le fichier us, traduire et si possible nommer comme j'ai fait)
 * Il y a encore des modifications à faire.
 * Nouveau système de limitation de caractères dans la fenêtre pour ajouter des cheats codes (l'ancien est toujours disponible via le bouton, utilisez la checkbox pour le nouveau).
 * Il n'y a plus de vérification avant de demander à l'utilisateur que faire, il y a une vérification en cas de copie uniquement.

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
See english version
