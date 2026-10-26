# TP2 LOG8704 – Système solaire de poche (AR mobile)

Application Android en réalité augmentée faite avec Unity et AR Foundation pour le TP2. On peut poser la Terre, Mars et Jupiter sur une surface et les manipuler. Quand on touche une planète, sa fiche s'affiche, et quand on s'en approche, son nom et la distance apparaissent. Il y a aussi une carte à imprimer qui fait apparaître le Soleil.

## Scène à compiler

La scène à compiler est `Assets/Scenes/SystemeSolaire.unity`. C'est la seule qui est cochée dans **File > Build Profiles > Scene List**.

## Configuration

| Élément | Version / valeur |
| --- | --- |
| Unity | 6000.3.23f1 |
| AR Foundation / Google ARCore XR Plugin | 6.5.0 |
| XR Interaction Toolkit | 3.5.1 |
| Plateforme | Android (ARM64, API 34 minimum) |
| Orientation | Portrait |

## Compiler et installer

1. Ouvrir le projet avec Unity 6000.3.23f1. Il faut avoir installé le module Android Build Support avec OpenJDK et Android SDK & NDK Tools.
2. Aller dans **File > Build Profiles**, sélectionner **Android** et cliquer sur **Switch Platform**.
3. Sur le téléphone, activer le mode développeur et le débogage USB, puis le brancher à l'ordinateur.
4. Sélectionner le téléphone dans **Run Device** et faire **Build And Run**.

## Utilisation

Le bandeau en haut de l'écran donne les consignes une par une :

1. Scanner une table en bougeant le téléphone lentement.
2. Choisir une planète avec le bouton **+**, puis toucher la table pour la poser.
3. Toucher la planète : elle fait un tour sur elle-même et sa fiche s'affiche. Un 2e tap cache la fiche.
4. Avec un doigt on déplace la planète, avec deux doigts on la tourne, et en pinçant on change sa taille (entre la moitié et le double de la taille de départ).
5. Approcher le téléphone à moins de 40 cm d'une planète pour voir son nom et la distance en temps réel. C'est notre fonctionnalité supplémentaire (interaction basée sur la position du téléphone).
6. Viser la carte Soleil pour faire apparaître un Soleil 3D dessus.

Le bouton en haut à droite permet de tout réinitialiser.

## Marqueur

Il faut imprimer  `Assets/Textures/CarteSoleil.png`.

Dans `Assets/AR/BibliothequeImages`, la taille physique est réglée à 0,1 m. Si vous imprimez la carte à une autre taille, il faut changer la valeur **Physical Size** dans la bibliothèque, sinon la détection marche moins bien.

## Scripts (`Assets/Scripts`)

| Script | Ce qu'il fait |
| --- | --- |
| `AncragePlanete` | Crée un ARAnchor pour chaque planète posée et refait l'ancrage après chaque déplacement |
| `TapPlanete` | Fait la différence entre un tap et un glisser, fait tourner la planète et ouvre ou ferme sa fiche. |
| `FaceCamera` | Tourne les fiches et les étiquettes vers la caméra|
| `ProximitePlanete` | Affiche le nom et la distance quand le téléphone est à moins de 40 cm  |
| `RotationContinue` | Fait tourner le Soleil sur lui-même. |
| `AffichageSelonSuivi` | Affiche le Soleil seulement quand la carte est vraiment suivie par ARCore |
| `GuideUtilisateur` | Gère les consignes, les messages quand le suivi est perdu et le bouton Réinitialiser |

## Crédits

- Textures des planètes et du Soleil : [Solar System Scope](https://www.solarsystemscope.com/textures/)
- Le projet est basé sur le modèle Unity « AR Mobile ».
