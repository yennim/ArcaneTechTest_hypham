# 🛠️ Projet Éditeur de Scène Unity - Remise Finale 
(Texte initialement généré par AI avec mon promptm suivi de modifications manuelles)

### I. Démarrage Rapide

* **Version Unity :** `6000.0.62f1` (il s'agissait de la dernière version LTS disponible)
* **Scène principale :** `Launcher.unity`
* **Lancement :** Ouvrir la scène dans Unity et appuyer sur **Play**.
* **Interface :** Le header concerne l'édition ou sélection de projet. Le menu en bas à gauche contient les éléments représentant les primitives à drag and drop dans la salle. Cliquer sur un modèle existant 

---

### II. Fonctionnalités Implémentées

Les objectifs de base de l'éditeur ont été atteints :

* **Gestion de Projet :** Les fonctionnalités de **Création (Nouveau Projet), Renommage, Sauvegarde, Visualisation des projets enregistrés et Chargement** de l'état de l'environnement sont complètes.
* **Instanciation (Drag & Drop) :** Lancement de modèles dans la scène.
    * **Contrôle de Collision :** L'instanciation est automatiquement **annulée** si le modèle chevauche un objet existant.
* **Manipulation d'Échelle :** L'UI permet de modifier l'échelle :
    * **Uniformément** via un Slider.
    * Par **valeur directe** sur chacun des axes (X, Y, Z).
* **Couleur :** Changement de la **couleur totale du matériau** pour l'objet sélectionné.
* **Suppression :** Possibilité de supprimer les modèles sélectionnés.

---

### III. Décisions et Compromis Techniques

| Fonctionnalité | Justification de la Décision |
| :--- | :--- |
| **Modèles 3D** | Utilisation des **Primitives Unity** pour la majorité. J'ai conservé une tentative de génération de `Mesh` pour la Pyramide, même si son résultat est **imparfait** (artefacts visuels possibles). |
| **Couleur par Face** | Simplifiée en **coloration totale de l'objet**. Après des tentatives non concluantes avec les index de triangles (potentiel problème de shader), j'ai opté pour le *fallback* afin de ne pas risquer de perdre du temps qui serait bénéfique aux autres fonctionnalités. J'ai laissé tout de même le code qui récupérait le triangleIndex sélectionné, j'ai laissé cet index affiché lorsque l'on clique deux fois sur un objet. Je ne sais pas si c'était un problème de shader ou de logique dans ce que j'avais essayé. |
| **Contrôle d'Échelle** | Le Slider d'échelle uniforme n'a pas de limite dynamique (détection de collision maximale), ce que j'aurais aimé ajouter pour éviter que le scaling ne finisse par faire overlapper des objets déjà positionnés mais je ne m'y suis pas rendue dans ma priorisation. |
| **Placement d'objets** | J'ai utilisé le system d'input legacy car j'y étais plus familière que le nouveau qui m'aurait pris du temps a prendre en main. J'ai implémenté une vérification pour annuler le placement d'un objet s'il est superposé avec un autre, mais cette vérification n'est pas faite avec les murs (je viens d'y penser maintenant en rédigeant ce readme). |
| **Gestion de la physique** | Dans le contexte d'un editeur permettant de placer des objets dans l'espace, j'ai pensé qu'on voudrait contraindre les mouvements des objets pour ne pas qu'ils soient poussés ou déplacés sans l'interaction explicite de l'utilisateur. |
| **Déplacer un objet** | La fonctionnalité de **déplacement direct (`Move`)** d'un modèle après son instanciation n'a pas été implémentée, car je l'avais placé plus tard dans les priorisations (car ce n'était pas parmi requis explicites) et je ne m'y suis pas rendue. |

---

### IV. Bug majeur détecté avant la remise

Je ne connais pas les étapes de reproduction exactes, je crois que c'est le fait de loader un projet existant, et peut-être que le premier objet sélectionné est affecté?
Le bug en question est que parfois, le fait de **sélectionner un objet le fait disparaître visuellement** bien qu'il soit encore dans la hiérarchie. Peut-être lié aux meshes ou quelque chose du genre, et ça reste invisible si je resélectionne un autre projet puis reviens dessus. Mais je n'ai pas eu plus de temps pour m'y pencher...
Il semble possible de sélectionner un autre objet du projet juste après par contre et même de le sauvegarder.

---

### V. Processus

J'ai travaillé sur le projet en suivant le processus suivant:

* **Planification : ** J'ai fait une liste de tâches associées aux requis, en créant certaines tâches en étapes MVP vs amélioration. J'ai priorisé ces tâches en partant initialement avec l'idée de faire une version minimale de toutes les fonctionnalités, puis de faire une itération plus acceptable par la suite. Suivant cette priorisation, tout ce qui est intégration de UI esthétique était très bas dans les priorités.
* **Diagramme : ** J'ai élaboré des drafts de diagrammes pour savoir par où commencer, notamment en commençant par clarifier les classes de Model vs ModelController et de Project vs ProjectController pour m'assurer de la fonctionnalité de sauvegarde. L'implémentation finale ne suit pas à la lettre ces diagrammes comme le temps était limité, ces diagrammes ont servi à me lancer.
* **Implémentation des classes** 
* **Intégration des menus UI barebone, séparés en 3 sous-UI (prefab). Instanciation par clic à une position.**
* **Sauvegarde/chargement**
* **Changement d'échelle par axes et uniformément**
* **Changement de couleur par face -> infructueuse. Implémentation du fallback matériau complet. Redirection des efforts sur d'autres points du projet**
* **Drag and Drop**
* **Refonte de l'interface (projets et edition de modele)**
* **Ajout de modèles, avec tentative de générer un mesh de pyramide par code.**
* **Restreindre le drag and drop sur des zones libres**
* **Ajout d'un delete model qui est rapide à implémenter en fin de projet**
* **Corrections sur des éléments remarqués en fin de projet (pas assez pour le bug mentionné plus haut :( )**
* **Documentation/Remise**

---

### VI. Tâches restantes (globalement)

* Bugfix l'objet qui disparaît
* Ajouter les murs dans la vérification de collision lors du drag and drop
* Coloration par face
* Déplacer un objet
* Voir possibilité d'ajout d'un max scale basé sur la collision avec les objets autour
* Pivoter un objet
* Supprimer un projet
* Confirmations d'action (proposer de sauvegarder avant de changer de projet)
* Clean up du code, réorganisation des méthodes ou variables au sein des fichiers.
