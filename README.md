# 🛠️ Projet Éditeur de Scène Unity - Remise Finale 
(texte généré avec mon prompt mais rédigé par AI sauf quelques corrections manuelles)

### I. Démarrage Rapide

* **Scène principale :** `Launcher.unity`
* **Lancement :** Ouvrir la scène dans Unity et appuyer sur **Play**.
* **Interface :** Les contrôles (Instanciation, Échelle, Projet) sont accessibles via l'UI en surimpression (overlay). En bas a gauche pour pour les modeles, le header pour les editions de projet, une interface en bas a droite apparaît après sélection d'un modèle pour pouvoir l'éditer.

---

### II. Fonctionnalités Implémentées

Les objectifs de base de l'éditeur ont été atteints et sont stables :

* **Gestion de Projet :** Les fonctionnalités de **Création (Nouveau Projet), Renommage, Sauvegarde et Chargement** de l'état de l'environnement sont complètes.
* **Instanciation (Drag & Drop) :** Lancement de modèles dans la scène.
    * **Contrôle de Collision :** L'instanciation est automatiquement **annulée** si le modèle chevauche un objet existant (collision), assurant le respect des règles de physique.
* **Suppression :** Possibilité de supprimer les modèles sélectionnés.
* **Manipulation d'Échelle :** L'UI permet de modifier l'échelle :
    * **Uniformément** via un Slider.
    * Par **valeur directe** sur chacun des axes (X, Y, Z).
* **Couleur :** Changement de la **couleur totale du matériau** pour l'objet sélectionné.

---

### III. Décisions et Compromis Techniques

Il y a un bug que j'ai remarqué seulement très tard, peut-être introduit après ou peut-être que c'était là mais que je n'avais pas assez manipulé l'édition dans un projet existant (bien qu'il me semblait l'avoir testé quelques fois). Ce bug est lié au fait qu'un objet semble disparaître bien qu'il soit encore dans la hiérarchie, il ne s'affiche plus. Peut-être lié aux meshes ou quelque chose du genre mais je n'ai pas le temps de m'y pencher... Il faut malheureusement replay dans cette situation et tester les fonctionnalités depuis un nouveau projet si ça arrive.

| Fonctionnalité | Justification de la Décision |
| :--- | :--- |
| **Modèles 3D** | Utilisation des **Primitives Unity** pour la majorité. J'ai conservé une tentative de génération de `Mesh` pour la Pyramide, même si son résultat est **imparfait** (artefacts visuels possibles). |
| **Couleur par Face** | Simplifiée en **coloration totale de l'objet**. Après des tentatives non concluantes avec les index de triangles (potentiel problème de shader), j'ai opté pour le *fallback* afin de ne pas risquer de perdre du temps sur des fonctionnalités secondaires. Le code de récupération des index est présent. |
| **Contrôle d'Échelle** | Le Slider d'échelle uniforme n'a pas de limite dynamique (détection de collision maximale) mais je voyais ça comme une amélioration, la solution actuelle étant fonctionnelle tout de même.|
| **CRUD** | La fonctionnalité de **déplacement direct (`Move`)** d'un modèle après son instanciation n'a pas été implémentée, car le temps restant a été alloué à la finalisation de la Sauvegarde/Chargement. |