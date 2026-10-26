# Analyse du projet d'équipe — TP2 LOG8704

Lecture seule du dépôt `C:\Users\Temporaire\TP2_LOG8704` (remote `https://github.com/Kapiix/TP2_LOG8704.git`), le 26 octobre 2026. Unity n'a pas été lancé. Tout ce qui dépend du téléphone est marqué « À vérifier sur le téléphone ».

## Résumé

Le dépôt d'équipe est le modèle Unity « AR Mobile », plus un marqueur (photo d'un phoque) qui affiche le texte « big ass seal », et un tap qui fait rebondir un cube. Le commit du 22 octobre retire aussi le composant qui sert à déplacer, tourner et redimensionner ce cube, et allume la gravité : le cube risque de traverser le sol. Il manque la fonctionnalité bonus, le README, le rapport, la vidéo et le commit de remise. Une seule branche `main`, rien de non commité, pas de conflit. Recommandation : pars de ton projet (planètes, fiche au tap, marqueur Soleil, déjà testés sur ton téléphone), copie ta scène dans ce dépôt pour la remise, et termine l'étiquette de proximité comme fonctionnalité bonus.

---

## 1. Historique Git

Commandes exécutées dans `C:\Users\Temporaire\TP2_LOG8704` :

- `git status` : branche `main`, à jour avec `origin/main`, working tree clean (avant l'écriture de ce fichier).
- `git branch -a` : uniquement `main` et `remotes/origin/main` (`origin/HEAD` pointe sur `origin/main`). Aucune branche non fusionnée.
- `git stash list` : vide.
- Recherche de `<<<<<<<` dans le dépôt : aucun marqueur de conflit.
- `git shortlog -sn --all` : 3 commits, tous de **Kapiix**. Ton nom n'apparaît pas.
- Remote : `https://github.com/Kapiix/TP2_LOG8704.git`.

Le message du dernier commit dit « voir discord pour les TODO ». Ces TODO ne sont pas dans le dépôt. Impossible de les lire.

### `e0c1dd55b935fc06a455a6d9f347750a6bcc7cf2` — 2026-10-14 23:25:28 -0400

- Auteur : Kapiix `<85250219+Kapiix@users.noreply.github.com>`
- Message : « Enregistrement initial »
- Fait vraiment : importe le modèle Unity AR Mobile complet. 800 fichiers, +223020 lignes. Scène, interface de coaching, prefabs de formes, paquets AR. Aucune fonctionnalité écrite par l'équipe.

### `94c67faf52b40c637df93a13d3bb1f6d04789ead` — 2026-10-14 23:29:15 -0400

- Auteur : Kapiix `<pierrecarre@live.fr>`
- Message : « Mise à jour du projet »
- Fait vraiment : passe les images, modèles, sons et polices en **Git LFS**. Dans l'historique Git, ces fichiers passent de plusieurs mégaoctets à environ 130 octets (le pointeur LFS). Exemple : `Assets/MobileARTemplateAssets/Materials/Concrete_Normal.tif` passe de 1 707 440 octets à 132 octets dans ce commit.
- Le diff texte utile se limite à : numéro de version du fichier URP (`Assets/Settings/URP-Performant.asset`, `k_AssetVersion` 12 → 13) et l'identifiant cloud Unity (`ProjectSettings/ProjectSettings.asset` : `projectName: TP2_LOG8704`, `organizationId: pierrecarre05`, `cloudProjectId: a36ae3da-0c03-478e-a82e-d87747d501da`).
- Les gros fichiers du premier commit restent dans l'historique. Un clone complet les télécharge encore, en plus des fichiers LFS.
- Sur cette machine, LFS a bien remis les vrais fichiers : `git lfs ls-files` (90 fichiers) n'a aucune ligne « pas téléchargé », et `Assets/Resources/FFS.png` commence par l'en-tête PNG.

### `23648e8392e1995daa0a7ad16b70f04044d6538d` — 2026-10-22 17:10:44 -0400

- Auteur : Kapiix `<pierrecarre@live.fr>`
- Message : « Execution d'une grosse partie du TP (voir discord pour les TODO) »
- 75 fichiers, +3849 / −526. La majorité des lignes sont des `.meta` de sprites réécrits par Unity et la police TextMeshPro. Le travail réel, vérifié dans le diff `94c67fa..23648e8` :

| Changement réel | Preuve |
| --- | --- |
| Ajoute `Assets/ImageRecognition.cs` et `Assets/AR_Tap_Manager.cs` | fichiers présents, créés dans ce commit |
| Ajoute le marqueur `Assets/ReferenceImageLibrary.asset` et la photo `Assets/Resources/FFS.png` | idem |
| Ajoute le prefab texte `Assets/Prefabs/ImageInfo.prefab` | idem |
| Pose `ARTrackedImageManager` + `ImageRecognition` sur le rig AR de la scène | diff de `Assets/Scenes/SampleScene.unity` |
| Retire 6 formes de la liste d'objets à poser. Il ne reste que `CubeVariant` | même diff : 6 lignes `m_ObjectPrefabs` supprimées |
| Sur `CubeVariant` : gravité allumée, corps non cinématique, **retrait de `XRGrabInteractable`**, ajout du script de rebond et d'un `XRSimpleInteractable` vide | diff de `Assets/MobileARTemplateAssets/Prefabs/CubeVariant.prefab` |
| `ARFeatheredOcclusionPlane.prefab` : 4 champs ajoutés par une nouvelle version de Unity (`m_ForceMeshLod`, etc.). Aucun changement de comportement | diff du prefab |
| `ProjectSettings` : batching statique Android activé | diff, `m_StaticBatching: 1` |
| Ajoute `.vscode/` (réglages d'éditeur) | commit |

---

## 2. Configuration du projet

### Unity

`ProjectSettings/ProjectVersion.txt` : **6000.3.23f1** (révision `09d2ecc7fb28`). C'est Unity 6.3.

### Paquets (`Packages/manifest.json` et `packages-lock.json`)

| Paquet | Version |
| --- | --- |
| AR Foundation | 6.5.0 |
| ARCore (Android) | 6.5.0 |
| ARKit (iOS) | 6.5.0 |
| XR Interaction Toolkit | 3.5.1 |
| XR Management | 4.6.1 |
| Input System | 1.20.0 |
| Universal Render Pipeline | 17.3.0 |
| uGUI (TextMeshPro est inclus dedans en Unity 6) | 2.0.0 (`packages-lock.json` ligne 211) |

Pas de paquet de suivi des mains, du visage, ni de multijoueur AR. `com.unity.multiplayer.center` est le panneau vide livré avec Unity.

### Scène du build

`ProjectSettings/EditorBuildSettings.asset` lignes 7-10 : une seule scène, activée, en première position :

`Assets/Scenes/SampleScene.unity` (guid `4755b35b590504809a7525fd2109c2e2`).

Ces scènes existent et ne sont pas dans le build : `Assets/Settings/Project Configuration/SampleScene.unity`, `Assets/Samples/.../ARDemoScene.unity`, `Assets/Samples/.../DemoScene.unity`.

### Réglages Player (`ProjectSettings/ProjectSettings.asset`)

| Réglage | Valeur | Ligne | Sens |
| --- | --- | --- | --- |
| Nom du produit | `TP2_LOG8704` | 16 | nom sous l'icône |
| Compagnie | `DefaultCompany` | 15 | valeur du modèle |
| Identifiant Android | `com.unity.template.ar_mobile` | 173 | identifiant du modèle, pas un identifiant d'équipe |
| Orientation | `0` = Portrait | 11 | le téléphone reste à la verticale. Les cases d'auto-rotation (lignes 62-65) ne servent que si l'orientation est « Auto » |
| API Android minimum | 34 (Android 14) | 184 | un S26 Ultra est plus récent : compatible |
| API cible | 0 (automatique) | 185 | |
| Architectures | 2 = ARM64 seulement | 275 | bon pour le S26 Ultra |
| Scripting backend Android | 1 = IL2CPP | 846 | |
| Input | 1 = nouveau Input System seulement | 939 | compatible avec `AR_Tap_Manager.cs` |
| Espace couleur | 1 = linéaire | 50 | |

### XR Android

`Assets/XR/XRGeneralSettings.asset` : le bloc « Android Settings » (ligne 109) a `m_InitManagerOnStart: 1` et charge le loader guid `bbf1bbcc68185fe41b723ad39ec4d68e`.

Ce guid est celui de `Assets/XR/Loaders/AR Core Loader.asset.meta`. ARCore est donc le plugin actif sur Android.

`Assets/XR/Settings/AR Core Settings.asset` ligne 16 : `m_Requirement: 0`. Dans `ARCoreSettings.cs` du paquet, 0 = **Required** (l'application exige ARCore).

### `.gitignore`

`Library/`, `Temp/`, `Obj/`, `Build/`, `Builds/`, `Logs/`, `UserSettings/` sont ignorés (`.gitignore` lignes 17-23). `git ls-files Library` ne retourne rien. Ces dossiers existent sur le disque parce que le projet a été ouvert, ils ne sont pas dans Git.

Les binaires du modèle sont suivis par LFS (`.gitattributes` lignes 73-160 : png, jpg, fbx, wav, ttf, pdf, etc.). Le gros fichier encore stocké en entier dans Git est la police TextMeshPro, parce qu'un `.asset` n'est pas dans les règles LFS : `Assets/TextMesh Pro/Resources/Fonts & Materials/LiberationSans SDF.asset`, environ 2,2 Mo, déjà dans le premier commit.

---

## 3. Scène, prefabs, marqueur

La scène compilée est `Assets/Scenes/SampleScene.unity`. Les objets qui comptent :

### `XR Origin (AR Rig)`

Instance du prefab `Assets/Samples/XR Interaction Toolkit/3.5.1/AR Starter Assets/Prefabs/XR Origin (AR Rig).prefab` (guid `48cb3fb68c91eb94999fd99957eb0cae`, scène ligne 1230).

Composants du prefab, sur l'objet racine :

| Composant | Guid du script | Où |
| --- | --- | --- |
| XR Origin | `e0cb9aa70a22847b5925ee5f067c10a9` | prefab, ligne 244 |
| AR Plane Manager | `e1760703bbd54c04488a8d10600262ab` | prefab, ligne 275. Le guid correspond à `ARPlaneManager.cs.meta` dans le paquet |
| AR Raycast Manager | `fa17d122634046b4a8e23048891fafc5` | prefab, ligne 289 |

Sur l'enfant `Main Camera` : caméra, `ARCameraManager` (guid `4966719baa26e4b0e8231a24d9bd491a`), fond AR, et le suivi de position du casque/téléphone (`TrackedPoseDriver`, guid `c2fadf230d1919748a9aa21d40f74619`, `m_TrackingType: 0` = position et rotation).

La scène ajoute sur ce rig (`SampleScene.unity` lignes 1220-1226) :

- `ARTrackedImageManager` (guid `4dea56a9eaadd1c439095de4f9a371cb`), lignes 594-602
- `ImageRecognition` (guid `351b7511b7600dd48a46703b113b16ed`, le `.meta` de `Assets/ImageRecognition.cs`), lignes 603-617

Réglages du gestionnaire d'images :

- bibliothèque : guid `69f5943fcdf8b4149b2ac0579d4b7adc` = `Assets/ReferenceImageLibrary.asset.meta`
- prefab affiché : guid `85307e6fd70246346b211d748fe07bec` = `Assets/Prefabs/ImageInfo.prefab`
- `m_MaxNumberOfMovingImages: 1` (le marqueur peut bouger)
- entrée du script : `imageName: FFS`, `text: big ass seal` (lignes 615-617)

Le plan détecté utilise `Assets/MobileARTemplateAssets/Prefabs/ARFeatheredOcclusionPlane.prefab` (guid `886609fc1566e4c27b399f1095d6de18`, scène ligne 1169). Ce prefab a un `MeshCollider` **non convexe** (`m_Convex: 0`, `m_IsTrigger: 0`, lignes 56-60).

Il n'y a pas de `ARAnchorManager` dans la scène ni dans le prefab du rig.

### `AR Session`

`SampleScene.unity` ligne 2165, script guid `fa850fbd5b8aded44846f96e35f1a9f5`. C'est la session AR du modèle. Elle est nécessaire : sans elle, la caméra AR ne démarre pas.

### `Object Spawner`

`SampleScene.unity` lignes 1450-1548.

- Script `ObjectSpawner` : guid `956dd6cf70eaca449a45b6a95b96c8c1` = `Assets/Samples/.../Starter Assets/Scripts/ObjectSpawner.cs.meta`
- Une seule entrée dans `m_ObjectPrefabs` (ligne 1470) : guid `d825bbdf58c379d4290da92e387f71d0` = `CubeVariant`
- `m_SpawnOptionIndex: 0`, `m_SpawnAsChildren: 1`, `m_OnlySpawnInView: 1`
- `m_CameraToFace` est vide (ligne 1468). Le script prend `Camera.main` si le champ est vide (`ObjectSpawner.cs` lignes 13-24). La caméra du rig a le tag `MainCamera`. Ce champ vide est donc prévu.
- `ARInteractorSpawnTrigger` (guid `ba63293f961b46a1a5b648e4ba02cfb5`), `m_SpawnTriggerType: 0` = `SelectAttempt` (`ARInteractorSpawnTrigger.cs` lignes 19-25). Un tap sur une surface qui ne sélectionne rien fait apparaître l'objet.
- L'objet spawner est enfant de `Camera Offset` (scène, `m_AddedGameObjects`, ligne 1217). C'est déjà ainsi dans le commit initial. La caméra bouge grâce au `TrackedPoseDriver` ; le parent `Camera Offset` reste à l'origine. Les objets posés ne sont donc pas collés à l'écran, tant qu'ils restent cinématiques.

Dans le commit initial, la liste contenait 7 prefabs. Le commit `23648e8` en retire 6. Les fichiers sont encore sur le disque, ils ne sont plus proposés :

| Index d'origine | Prefab | Guid |
| --- | --- | --- |
| 0 (encore dans la liste) | `CubeVariant` | `d825bbdf58c379d4290da92e387f71d0` |
| 1 | `PyramidVariant` | `3cbfcd4079a5031478389d5ab3c8c311` |
| 2 | `TorusVariant` | `23412048bf4821d4f8bf5c7d9c1abd5b` |
| 3 | `WedgeVariant` | `aaf6326249753074fbc1148303020f26` |
| 4 | `ArchVariant` | `b1b37ac0eaa6a9a4785fc1f26065e8b3` |
| 5 | `CylinderVariant` | `daca7e1cd0a975941a64a8a9cbd8e5e5` |
| 6 | `DebugCubeVariant` | `98cdacc32bf0d8d469092762a7263c4b` |

Les boutons de l'interface appellent encore `SetObjectToSpawn` avec ces index (`SampleScene.unity`) : Cube = 0, Pyramid = 1, Torus = 2, Triangle = 3, Arch = 4, Cylinder = 5, Cube Debug = 6. `ARTemplateMenuManager.SetObjectToSpawn` (lignes 329-335) ignore un index plus grand que la liste et écrit un avertissement. Seul le bouton Cube change vraiment l'objet. Les six autres formes ont encore leur `XRGrabInteractable` d'origine : elles seraient déplaçables, mais on ne peut plus les faire apparaître.

### `CubeVariant` — le seul objet que l'on peut poser

Fichier : `Assets/MobileARTemplateAssets/Prefabs/CubeVariant.prefab`. C'est une variante de `Assets/Samples/.../DemoAssets/Prefabs/Cube.prefab` (guid `ebd7292e888c84f05924605e51048072`).

Le cube d'origine a :

- un `Rigidbody` cinématique, gravité éteinte (`m_UseGravity: 0`, `m_IsKinematic: 1`, lignes 63-64)
- `XRGrabInteractable` (guid `0ad34abafad169848a38072baa96cdb2`, confirmé dans `Library/PackageCache/.../XRGrabInteractable.cs.meta`), fileID `2996392715678626154`
- `ARTransformer` (guid `b309761041ee54265b7f9a8c0f373dab`), échelle de 0,25 à 2 (lignes 213-214). C'est ce composant qui limite le pincement. Il ne travaille que pendant une prise `XRGrabInteractable`
- un enfant `Cube_COL` avec un `MeshCollider` convexe (ligne 270)

La variante, modifiée par le commit `23648e8`, fait ceci :

- `m_UseGravity: 1` et `m_IsKinematic: 0` (lignes 71-78)
- `m_MinScale: 0.3` (ligne 84)
- **retire** `XRGrabInteractable` (`m_RemovedComponents`, lignes 87-88)
- ajoute `AR_Tap_Manager` avec `bounceForce: 2` (lignes 104-116, guid `ef691ed02c9756347aa1e5d291fc2019`)
- ajoute `XRSimpleInteractable` (guid `8a35f6cfbfba9b548aaa00d52cfe8a50`) avec `m_Colliders` vide et toutes les listes d'événements vides (lignes 117-190)

Conséquence lisible dans les fichiers : on ne peut plus attraper le cube pour le déplacer, le tourner ou le redimensionner. `ARTransformer` est encore sur le prefab de base, et plus rien ne l'appelle. Avec la gravité allumée, le cube est un corps physique. Le sol AR est un mesh collider non convexe. Unity ne fait pas collisionner un corps dynamique avec un mesh collider non convexe : le cube a de fortes chances de tomber à travers le plan. À confirmer sur le téléphone, Unity n'a pas été lancé.

### Bibliothèque d'images

`Assets/ReferenceImageLibrary.asset` :

- type `XRReferenceImageLibrary` (script guid `848409ee8ba454f42a5ddff137688a27`)
- une image, nom `FFS`
- taille physique demandée : **0,50 m × 0,343 m** (`m_SpecifySize: 1`, ligne 27)
- `m_Texture: {fileID: 0}` (ligne 29). Ce n'est pas un lien cassé : AR Foundation peut ne pas garder la texture dans l'application, et stocker seulement son guid (`XRReferenceImageLibraryExtensions.SetTexture`, paramètre `keepTexture`)
- le guid de texture calculé depuis `m_SerializedTextureGuid` (`12994153509284184612` / `883046614817907816`) vaut `4dc7822481c5b454682455b43736410c`, identique à `Assets/Resources/FFS.png.meta` ligne 2
- `m_DataStore.m_Storage` est vide (ligne 18). Le paquet ARCore le dit lui-même : ces données sont recalculées au moment du build Android (`ARCoreImageLibraryBuildProcessor.cs`). Un dépôt avec un stockage vide est normal. Le build peut échouer plus tard si l'outil `arcoreimg` rejette la photo ; ça ne se voit qu'en compilant.

`Assets/Resources/FFS.png` : vrai PNG, 1 061 601 octets, 1268 × 870 pixels. Rapport largeur/hauteur 1,4575, identique à 0,50 / 0,343. La photo est un phoque en peluche sur fond blanc. Les taches grises donnent du détail, le grand fond blanc rend la détection moins sûre. À vérifier sur le téléphone, en imprimant l'image à **50 cm de large**. Une impression A4 (21 cm) ne correspond pas à la taille enregistrée : la position du texte sera fausse même si le phoque est reconnu.

### Prefab affiché sur le marqueur

`Assets/Prefabs/ImageInfo.prefab` : un objet `ImageInfo` et un enfant `ImageInfoChild`.

- L'enfant est un texte 3D TextMeshPro (guid `9541d86e2fd84c1d9990edf0852d74ab`), police guid `8f586378b4e144a9851e7b34d9b748ee` = `LiberationSans SDF.asset.meta`
- Le texte dans le prefab est vide (ligne 135). `ImageRecognition` le remplit à l'exécution
- Rotation locale 90° sur X (ligne 60) : le texte est à plat sur le marqueur, il ne se tourne pas vers le téléphone
- Échelle 0,02 et rectangle 20 × 5 : environ 40 cm × 10 cm
- Pas de modèle 3D, pas de collider, pas d'animation

### Interface

Objet `UI` (ligne 7686) : Canvas en mode écran, scaler, `GoalManager` (guid `30f0e07d95a4d497bbc3dbd22d792191`, activé), `ARTemplateMenuManager` (guid `f708c04c3dca17f4dbdc065df6a0c397`, **`m_Enabled: 0`**, ligne 7823).

Le menu désactivé au démarrage vient du modèle. `GoalManager` le rallume quand le tutoriel commence (`GoalManager.cs` ligne 311). Ce n'est pas une casse du commit du 22 octobre.

Cartes du tutoriel, dans l'ordre de `m_StepList` (lignes 7794-7809) :

1. `Prompt_ScanSurfaces` — « Move device to scan the environment »
2. `Prompt_TapToPlace` — « Touch surfaces to place objects »
3. `Prompt_MoveObject` — « Drag objects with one finger »
4. `Prompt_ScaleObject` — « Pinch objects to change their size »
5. `Prompt_RotateObject` — « Use a twist gesture to rotate objects »

Les textes sont dans les prefabs sous `Assets/MobileARTemplateAssets/UI/Prefabs/`. Les cartes 3, 4 et 5 décrivent des gestes qui ont besoin de `XRGrabInteractable`. Ce composant a été retiré du seul objet que l'on peut poser. L'interface guide donc vers des gestes que le cube ne peut plus faire.

Autres boutons branchés dans la scène : Options (`ShowHideModal`), afficher les surfaces, menu debug AR, « Remove All Objects » (`ClearAllObjects`), indice (`GoalManager.StartCoaching`). Textes visibles : « Show Interaction Hints », « Visualize Surfaces », « Remove All Objects », « Cancel », « AR Debug Menu » (lignes 4577, 5013, 6544, 6756, 7194). Tout est en anglais, c'est le texte du modèle.

### Références cassées

- Les guid de scripts cités plus haut ont un fichier `.cs.meta` ou un script de paquet.
- La texture du marqueur correspond au PNG.
- Le prefab `ImageInfo` et la police existent.
- `SceneTemplate_RotateCube` (guid `6f17310979bb8bc409d7bc10ebea601c`) n'est référencé par aucune scène ni prefab. C'est un script du modèle de projet Unity, inutilisé.
- Aucun `<<<<<<<` laissé dans les fichiers.

---

## 4. Scripts écrits par l'équipe

Hors `MobileARTemplateAssets`, `Samples`, `TextMesh Pro` et les paquets, il y a deux scripts. `Assets/Settings/Project Configuration/SceneTemplate_RotateCube.cs` fait tourner un cube dans une scène d'exemple Unity. Il n'est attaché nulle part. Ce n'est pas un script de l'équipe.

### `Assets/ImageRecognition.cs` (40 lignes)

Attaché au rig AR dans `SampleScene.unity` (lignes 603-617), à côté de `ARTrackedImageManager`. Le script exige ce composant (`[RequireComponent]`, ligne 6).

Rôle : quand une image suivie est ajoutée ou mise à jour, il cherche un texte dans les enfants et, si le nom de l'image vaut une entrée de la liste, il écrit ce texte. Il cache le texte quand l'état n'est pas `Tracking` (lignes 30-38).

La liste dans la scène a une entrée : nom `FFS`, texte `big ass seal`. Le nom est le même que dans la bibliothèque. Le branchement nom ↔ texte est cohérent.

Points à connaître :

- Il utilise `trackablesChanged` (lignes 21-22). C'est la bonne API d'AR Foundation 6. L'ancienne `trackedImagesChanged` n'est pas utilisée. Pas d'erreur de compilation visible dans ce fichier.
- Il ne traite pas `args.removed`. AR Foundation détruit en général le prefab quand l'image est perdue pour de bon. Le texte, lui, est déjà caché dès que le suivi n'est plus `Tracking`.
- Si le texte est introuvable, la méthode sort (ligne 33). Le prefab a bien un `TMP_Text`.
- Le texte reste à plat. Rien ne le tourne vers la caméra.
- Le contenu est une blague, pas une fiche utile.

Ça ne se vérifie qu'en build Android : la base d'images ARCore est fabriquée à la compilation.

### `Assets/AR_Tap_Manager.cs` (62 lignes)

Attaché seulement à `CubeVariant` (prefab, lignes 104-116), donc seulement au cube posé.

Rôle : à chaque nouveau toucher (ou clic de souris dans l'éditeur), si le doigt n'est pas sur l'interface, il lance un rayon. Si le rayon touche le cube ou un de ses enfants, il pousse le `Rigidbody` vers le haut avec une force 2 (`bounceForce`, ligne 116 du prefab ; `OnTapped`, lignes 57-60).

Points à connaître :

- Le `Rigidbody` est bien sur le même objet dans le prefab d'origine. `GetComponent<Rigidbody>()` (ligne 19) le trouve. Avec la gravité allumée par la variante, la poussée le fait sauter, puis la gravité le ramène. Comme le sol AR n'arrête pas un corps dynamique, il peut traverser le plan. À vérifier sur le téléphone.
- `LastTapConsumedFrame` et `TapConsumedThisFrame` (lignes 9-10 et 59) ne sont lus nulle part ailleurs dans le projet. Le drapeau ne bloque pas le placement. Un même tap peut donc faire sauter le cube et, si le rayon AR ne « sélectionne » rien, faire apparaître un deuxième cube. Le `XRSimpleInteractable` ajouté n'a aucun événement : il n'affiche rien et ne joue aucune animation.
- Le rayon ignore l'interface avec `IsPointerOverGameObject(fingerId)` (ligne 28). Sur téléphone, l'identifiant du doigt de l'Input System et celui de l'EventSystem ne correspondent pas toujours. À vérifier : un tap sur un bouton pourrait aussi toucher le monde, ou l'inverse.
- Aucune fiche, aucune animation. Le tap ajoute une force. Pour la grille (« contenu additionnel »), c'est faible.
- Pas d'API AR Foundation obsolète dans ce fichier. Il compile avec l'Input System, et le projet est réglé sur l'Input System (`activeInputHandler: 1`).

---

## 5. Bilan par rapport au TP

| Exigence ou critère | État | Preuves | Ce qui manque |
| --- | --- | --- | --- |
| 1. Marqueur qui affiche du contenu (2 pts) | Partiel, à vérifier sur le téléphone | Bibliothèque `ReferenceImageLibrary.asset` (image `FFS`, 0,50 × 0,343 m, guid texture = `FFS.png`). `ARTrackedImageManager` scène lignes 594-602. Prefab `ImageInfo` = texte. Script `ImageRecognition.cs`. Commit `23648e8` | Le contenu est le texte « big ass seal », pas un modèle 3D. La base ARCore n'existe qu'après un build. Il faut imprimer la photo à 50 cm de large et tester la détection. Fond blanc : détection incertaine |
| 2. Placer et bouger l'objet, stable (3 pts) | Partiel | Placer : `ObjectSpawner` + `ARInteractorSpawnTrigger` (`SelectAttempt`), scène lignes 1456-1548, déjà dans le commit initial. Bouger : `XRGrabInteractable` retiré de `CubeVariant` lignes 87-88, commit `23648e8`. Gravité ligne 74, `m_IsKinematic: 0` ligne 78. Sol non convexe : `ARFeatheredOcclusionPlane.prefab` ligne 60. Pas de `ARAnchorManager` | Remettre la prise en main (déplacer, tourner, pincer avec les limites de `ARTransformer`). Remettre le cube cinématique, gravité éteinte, sinon il tombe. Tester la stabilité en marchant autour |
| 3. Tap qui déclenche du contenu additionnel (3 pts) | Partiel | `AR_Tap_Manager.cs` sur `CubeVariant`, force 2. Aucune lecture de `TapConsumedThisFrame`. `XRSimpleInteractable` sans événement | Remplacer le rebond par une fiche ou une animation. Empêcher ce tap de poser un deuxième objet |
| 4. Fonctionnalité supplémentaire (2 pts) | Absent | `Packages/manifest.json` : pas de mains, visage, météo, GPS, multijoueur. Un seul marqueur. Aucun script de distance ou d'orientation du téléphone en dehors du modèle | Choisir une option de l'énoncé et la relier au contenu. L'étiquette de proximité de ton projet correspond à « position du téléphone » |
| 5. Interface claire (3 pts) | Partiel | Tutoriel du modèle : 5 cartes, `GoalManager` sur `UI`. Menu d'objets, options, supprimer. Cartes 3 à 5 : `Prompt_MoveObject`, `Prompt_ScaleObject`, `Prompt_RotateObject`. Liste d'objets réduite à 1 prefab, boutons encore indexés 0 à 6 | Les cartes disent de glisser, pincer et tourner un cube qui ne le peut plus. Six boutons ne changent rien (`ARTemplateMenuManager.cs` lignes 334-335). Textes en anglais du modèle |
| 6. Informations utiles à l'utilisateur (2 pts) | Partiel | Cartes : « Move device to scan the environment », « Touch surfaces to place objects », etc. Boutons « Show Interaction Hints », « Visualize Surfaces », « Remove All Objects » | Le texte du marqueur (« big ass seal ») n'informe pas. Les cartes de gestes décrivent des gestes retirés. Rien n'explique le marqueur phoque |
| 7. Documentation (2 pts) | Absent | Aucun `README`, aucun rapport dans la racine. `git ls-files` ne contient pas ces documents | README (scène à compiler, téléphone testé) et rapport (intro, choix techniques, tests, améliorations) |
| 8. Vidéo 2 à 3 min (2 pts) | Absent | Aucun mp4/mov dans le dépôt. `.gitignore` ignore `Recordings/` | Tourner la vidéo une fois les fonctions stables sur le S26 Ultra |
| 9. Pas de bogue majeur ni de crash (1 pt) | À vérifier sur le téléphone | Gravité + collider non convexe : chute probable du cube. Tap et placement peuvent se déclencher ensemble. Référence image à valider au build | Un plantage ou une fonction qui ne marche pas compte comme non faite. Il faut un passage complet sur le téléphone |
| Livrable : README | Absent | racine du dépôt : `.gitattributes`, `.gitignore`, projets Visual Studio ignorés. Pas de README | À écrire |
| Livrable : rapport | Absent | aucun PDF/DOCX/MD de rapport dans le dépôt (ce fichier `ANALYSE_TP2.md` est ton analyse, pas le rapport du cours) | Intro, composants et scripts, tests, pistes |
| Livrable : vidéo | Absent | — | 2 à 3 minutes |
| Livrable : commit de remise | Absent | Dernier commit `23648e8` le 22 octobre, message « grosse partie du TP ». La remise est le 28 octobre 23h59 | Un commit explicite de remise, poussé sur GitHub, après les correctifs |

Estimation honnête si on rend le dépôt tel quel, sans test téléphone : le tuteur voit le modèle Unity, un texte de blague sur un phoque, et un cube qui saute. Les 3 points « placer et bouger » et les 3 points du tap sont en danger. Les 2 points bonus et les 6 points doc + vidéo sont à zéro.

---

## 6. Ce qui reste à faire

Ordre : ce qui rapporte le plus, et ce qui débloque le reste. Les durées sont pour quelqu'un qui débute, en travaillant dans Unity. Aujourd'hui on est le 26 octobre, la remise est le 28 à 23h59.

Les tests téléphone sont les mêmes pour chaque tâche : câble USB, options développeur et débogage USB sur le S26 Ultra, plateforme Android, ARM64, puis Build And Run. Tenir le téléphone à la verticale (l'application est verrouillée en portrait). Bonne lumière, bouger lentement au début pour laisser ARCore trouver le sol.

### 1. Remettre le déplacement du cube — 3 points, et ça évite le bogue de chute

Environ 2 h, plus 45 min de test.

Dans `Assets/MobileARTemplateAssets/Prefabs/CubeVariant.prefab` : remettre `XRGrabInteractable` (retirer la ligne de `m_RemovedComponents`), remettre `m_UseGravity` à 0 et `m_IsKinematic` à 1. `ARTransformer` (échelle 0,25 à 2 sur le cube d'origine, 0,3 en minimum sur la variante) redevient utile dès que la prise est là.

Fichiers : `CubeVariant.prefab`. La scène et `ObjectSpawner` peuvent rester.

Test : poser le cube (tap sur une table détectée), marcher autour (il ne glisse pas avec l'écran), un doigt pour le déplacer, deux doigts pour tourner, pincement pour la taille. Il ne doit pas traverser la table. Les cartes « Drag / Pinch / twist » redeviennent vraies : une partie des points d'interface revient avec ce seul correctif.

### 2. Faire du tap un vrai contenu — 3 points

Environ 4 h.

`Assets/AR_Tap_Manager.cs` pousse le cube. Le remplacer par une action visible : afficher une fiche (texte au-dessus du cube, tourné vers la caméra) ou lancer une animation. Brancher le script pour qu'un tap sur le cube ne pose pas un second cube (le drapeau `TapConsumedThisFrame` est écrit et jamais lu).

Tu as déjà ce comportement dans ton projet (`TapPlanete`, `FaceCamera`). Le réécrire ici est plus long que de réutiliser ta scène (voir section 7).

Test : tap sur le cube → la fiche apparaît. Tap sur la table à côté → un seul cube apparaît, la fiche ne s'ouvre pas. Tap sur un bouton de l'interface → rien ne se pose.

### 3. Documentation et vidéo — 2 + 2 points

README : 1 h. Rapport : 4 h. Vidéo : 2 h une fois que le téléphone montre les fonctions sans plantage.

README à la racine : scène `Assets/Scenes/SampleScene.unity`, Unity `6000.3.23f1`, Android, téléphone testé (modèle, version Android), marqueur à imprimer (`Assets/Resources/FFS.png` à 50 cm de large), comment poser / déplacer / tapper.

Rapport : introduction, composants (`ARSession`, `XR Origin`, `ARPlaneManager`, `ARRaycastManager`, `ARTrackedImageManager`, `ObjectSpawner`, `ARInteractorSpawnTrigger`, `XRGrabInteractable`, `ARTransformer`) et scripts (`ImageRecognition`, le script de tap), tests sur le S26 Ultra, pistes (ancre `ARAnchor`, plusieurs marqueurs, textes en français).

Vidéo 2 à 3 min : scan du sol, poser, déplacer, tourner, redimensionner, tap avec la fiche, montrer le marqueur imprimé et le contenu qui se colle dessus, montrer la fonction bonus. Éviter le texte « big ass seal » à l'image : ça se voit dans la remise.

Commit de remise ensuite, poussé sur `origin/main`. Message clair, par exemple « Remise TP2 LOG8704 ».

### 4. Rendre le marqueur présentable — 2 points

Environ 3 h, plus le test téléphone.

Garder `ARTrackedImageManager` et `ImageRecognition`. Changer le texte de la scène (lignes 616-617) pour une vraie phrase. Mieux : mettre un modèle 3D dans `ImageInfo` (ton Soleil le fait déjà). Imprimer `FFS.png` à 50,0 × 34,3 cm. Si l'impression est plus petite, changer `m_Size` dans la bibliothèque pour la vraie largeur en mètres, puis refaire un build (la base ARCore est recalculée à ce moment).

Test : la photo imprimée, à plat, bien éclairée, sans reflet. Le contenu apparaît au-dessus, suit la feuille quand on la tourne un peu, et disparaît ou se cache quand on cache la feuille. Si le build Android échoue sur `arcoreimg`, la console Unity donne l'erreur : la photo est alors refusée par ARCore et il faut une image plus contrastée.

### 5. Une fonctionnalité bonus — 2 points

Environ 5 h si tu la réécris ici. Moins si tu apportes `ProximitePlanete` depuis ton projet.

L'énoncé accepte, entre autres, un contenu qui dépend de la distance téléphone ↔ objet. Ton script de proximité (nom et distance sous 40 cm) rentre dans ce critère. Il faut l'attacher à l'objet posé et l'afficher face à la caméra.

Test : s'éloigner (l'étiquette est cachée), s'approcher à moins de 40 cm (nom + distance), reculer (elle se cache). Vérifier que le chiffre change en avançant.

### 6. Nettoyer l'interface — inclus dans les 3 + 2 points d'interface et d'informations

Environ 2 h.

Soit remettre les 6 prefabs dans `m_ObjectPrefabs` (ils ont encore la prise en main), soit retirer les boutons Pyramid, Torus, Triangle, Arch, Cylinder et Cube Debug pour qu'il ne reste que le cube. Ajouter une phrase qui dit quoi faire du marqueur. Le tutoriel anglais du modèle peut rester s'il dit la vérité après le correctif 1.

Test : chaque bouton visible pose bien la forme annoncée. Aucune carte ne demande un geste impossible.

### 7. Passage anti-plantage — 1 point

Environ 1 h 30, à la fin.

Scénario complet sans fermer l'application : lancer, accepter la caméra, suivre les 5 cartes, poser 3 objets, les bouger, tap, montrer le marqueur, s'approcher pour la proximité, supprimer les objets, recommencer. Si une de ces actions plante, la grille compte la fonction comme non faite.

---

## 7. Comparaison avec ton projet, et quelle base choisir

Tes scripts (`AncragePlanete`, `TapPlanete`, `FaceCamera`, `RotationContinue`, `AffichageSelonSuivi`, `ProximitePlanete`) ne sont pas dans ce dépôt, et une recherche dans `F:\Projets` ne les a pas trouvés. La comparaison ci-dessous suit ce que tu as décrit. Je n'ai pas ouvert ton projet Unity.

| Fonction notée | Projet d'équipe (fichiers lus) | Ton projet (ta description) |
| --- | --- | --- |
| Marqueur | Photo de phoque, texte « big ass seal » à plat. Branché, pas testé ici | Carte « Soleil » qui fait apparaître un Soleil 3D (`ARTrackedImageManager`, `RotationContinue`, `AffichageSelonSuivi`). Tu dis que ça marche sur ton téléphone |
| Placer et bouger | Le modèle sait poser un objet. Le commit du 22 octobre retire la prise du cube et allume la gravité | Terre, Mars, Jupiter : poser, déplacer, tourner, redimensionner avec limites, ancre `ARAnchor` via `AncragePlanete`. Testé sur ton téléphone |
| Tap | Une poussée physique, pas de fiche | La planète tourne et une fiche fait face à la caméra (`TapPlanete`, `FaceCamera`) |
| Bonus | Rien | `ProximitePlanete` en cours : nom et distance sous 40 cm. C'est bien une fonction de l'énoncé (position du téléphone) |
| Interface | Tutoriel du modèle, en anglais, en partie devenu faux | À compléter si ton interface est encore minimale. Le modèle d'équipe a déjà les cartes ; ton projet doit guider lui aussi, sinon les 3 + 2 points d'interface restent à faire |
| Doc, vidéo, commit | Absents des deux côtés, d'après ce dépôt | À produire pour la remise, quel que soit le projet choisi |
| Unity | 6000.3.23f1, AR Foundation 6.5.0, XRI 3.5.1, Android, ARCore obligatoire, API 34, ARM64, portrait | Unity 6, modèle AR Mobile, Android. Les numéros exacts sont dans ton `ProjectSettings/ProjectVersion.txt` et ton `Packages/manifest.json` |

### Recommandation : continuer ton projet, puis le déposer dans le GitHub de l'équipe

Ton projet couvre déjà, sur le vrai téléphone, les 2 + 3 + 3 points les plus risqués (marqueur, placement stable, tap utile). Le dépôt d'équipe a le tutoriel du modèle, et le commit du 22 octobre a cassé le déplacement que ce modèle fournissait. Réparer leur cube, réécrire une fiche, et refaire un marqueur correct prend le temps que tu as déjà passé de ton côté, avec le risque de tomber sur la gravité, le tap en double et la photo sur fond blanc.

Combiner les deux scènes à la main dans les fichiers YAML est le chemin le plus risqué. La façon sûre de combiner est de **copier ton travail fini dans leur dépôt**, sans mélanger les objets de leur scène.

Façon de le faire, avec le moins de risque :

1. Dans ton projet, note la version (`ProjectSettings/ProjectVersion.txt`) et les versions AR Foundation / ARCore / XR Interaction Toolkit (`Packages/manifest.json`). L'équipe est en Unity `6000.3.23f1`, AR Foundation `6.5.0`, XRI `3.5.1`. Si tu es sur une autre version Unity 6 proche, ouvre ensuite **leur** projet avec `6000.3.23f1` et importe tes assets : Unity convertit. Si les versions de paquets sont les mêmes, la copie est directe.
2. Copie uniquement tes fichiers à toi, **avec leurs `.meta`** (le `.meta` porte le guid ; sans lui, les liens de la scène cassent) :
   - tes scripts (`AncragePlanete`, `TapPlanete`, `FaceCamera`, `RotationContinue`, `AffichageSelonSuivi`, `ProximitePlanete`)
   - tes prefabs de planètes et du Soleil
   - ta bibliothèque d'images et la texture de la carte Soleil
   - ta scène, sous un nom nouveau, par exemple `Assets/Scenes/Scene_Planetes.unity`
3. Ne remplace pas `Assets/Scenes/SampleScene.unity` et ne modifie pas `CubeVariant`. Leur scène peut rester dans le projet. Elle ne sera plus celle du build.
4. Dans leur projet, File > Build Settings : décoche `SampleScene`, coche `Scene_Planetes`, place-la en position 0. Vérifie que Android, ARM64, API 34 et le loader ARCore sont toujours ceux décrits en section 2 (ils le sont déjà).
5. Termine `ProximitePlanete` dans cette scène. Ajoute 3 ou 4 textes d'aide à l'écran (scanner le sol, tap pour poser, tap sur la planète pour la fiche, approcher le téléphone pour le nom et la distance, montrer la carte Soleil). Ces phrases visent les points d'interface.
6. Build And Run sur le S26 Ultra. Scénario de la tâche 7.
7. README, rapport, vidéo, puis un commit de remise sur `main` et un push. Leur historique (3 commits) reste. Ton commit est celui de la remise.

Ce que tu laisses de côté, volontairement : le cube, le phoque, `AR_Tap_Manager`, `ImageRecognition`. Ils restent dans l'historique Git. Ils ne sont plus dans la scène compilée, donc ils ne peuvent plus faire tomber l'application.

Si l'enseignante exige que le dépôt de l'équipe soit la seule base et que ton projet perso ne compte pas, le plan B est la section 6 dans l'ordre 1 → 2 → 5 → 4 → 6 → 3 → 7, en repartant de `CubeVariant` et de `SampleScene`. C'est plus long, et la première heure sert à annuler une partie du commit `23648e8`.
