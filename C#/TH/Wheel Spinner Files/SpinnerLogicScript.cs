using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpinnerLogicScript : MonoBehaviour //Logic script for the wheel spinner (this game is not fully implemented yet)
{

    public float spinSpeed;
    public float decelerationSpeed;
    public bool isSpinning = false;
    public int count = 0;


    public Rigidbody2D rigidBody;
    public string[,] prizes;
    public int numberOfPrizes = 74;
    public TextMeshProUGUI prizeText, prizeTitleText;
    public GameObject awardPopUp;
    private List<int> availableNumbers;


    // Start is called before the first frame update
    void Start() // sets the prizes for the wheel
    {
        prizes = new string[,] 
        { { "50/50", "Un homme approche un d’entre vous qui a été choisi aléatoirement avec une potion et lui dit:  “Si tu bois cette potion, tu peux soit doubler ta puissance ou la diviser” S'il accepte le défi, il doit lancer une pièce dans les airs et si elle atterrit sur pile, tout son groupe  gagne 400 XP.  Si la pièce atterrit sur  face, tout le groupe perd  la moitié de leur coeurs." }, 
        {"Ah\"NOM\"!", "Le Maître du jeu a bu une potion magique par erreur, et cela le rend un peu confus aujourd’hui... Chaque fois que le Maître du jeu utilise le nom d'un élève, tous les joueurs gagnent 100 XP" },
        {"Bataille de champions", "Vous croisez un chevalier qui, par jalousie, attaque le plus fort d’entre vous. Le joueur avec le plus de X P perd 2 coeurs" },
        {"Bénédiction ou malédiction? ", "Vous trouvez une lame maudite… Un joueur sélectionné aléatoirement perd 5 cristaux mais gagne 300 XP" },
        {"Bienvenue dans la jungle!", "Le monde de Classcraft est rempli de surprises! Un joueur aléatoire par équipe perd un coeur" },
        {"Changement de fortune", "Vous avez de la chance et êtes destinés à de bonnes choses. Une équipe choisie aléatoirement gagne 300 XP" },
        {"Choisir de prospérer", "Un extraterrestre atterrit sur la terre et demande à parler à votre leader. Chaque équipe élit un coéquipier qui gagnera 300 XP" },
        {"Cloués sur vos sièges", "Certains guérriers décident d’observer les choses de loin. Les guerriers d'un groupe choisi aléatoirment doivent rester assis jusqu’au son de la cloche. S'ils le font bien, ils gagnent 500 Xp.  Sinon, ils perdent 300 XP" },
        {"Des bonnes manières", "Vous vous exercez pour la visite du roi. Tout le monde doit s’adresser aux autres en utilisant « votre majesté ». S'ils le font bien, ils gagnent 300 Xp.  Sinon, ils perdent 200 XP" },
        {"Énergie abondante", "Vous trouvez une potion mystérieuse et la donnez au plus faible. Le joueur avec le moins de cristaux dans chaque équipe gagne 3 cristaux." },
        {"Force brutale", "Certains guérriers s’exercent tous ensemble. Les guerriers d'un groupe choisi aléatoirment doivent rester debout pout toute la durée du cours. S'ils le font bien, ils gagnent 500 Xp.  Sinon, ils perdent 300 XP" },
        {"L’abondance règne", "Vos coéquipiers sont généreux et vous aident. Le joueur avec le moins de XP dans chaque équipe gagne 300 XP" },
        {"La Grande Niveleuse", "Elle se prononce et condamne… Tout le monde voient leurs coeurs tomber (ou monter) à 3." },
        {"L'attaque du méchant roi", "Un méchant Roi, qui vit dans un pays lointain, a perdu l’accès à l’internet royal et donc s’ennuie à ne rien faire.  Il décide alors de partir conquérir des pays avoisinants qu’il va choisir de façon aléatoire.  Il vous attaque et les guérriers de l’équipe choisie aléatoirement décident de se sacrifier au combat pour permettre au restant de la classe de vaincre ce méchant roi.  Ils perdent 2 coeurs mais reçoivent 300Xp pour leur bravoure." },
        {"Le pire Karaoké EVER!!", "Amusons-nous un peu… Le joueur avec le moins de XP choisit une chanson karaoke.  Le maître du jeu a 24h pour pratiquer la chanson.  Pour avoir fait ce choix judicieux, le joueur reçoit 300 XP." },
        {"Le chemin de la croissance", "Vous voulez réussir à affronter tous les obstacles devant vous sur la route du succès.  Vous devez vous entraîner. Cinq élèves choisis aléatoirement doivent répondre à une question du Maître du jeu. S'ils réussissent, ils gagnent 200 Xp et 100 PO.  Sinon, ils perdent 100 XP et 80PO" },
        {"Le corbeau noir", "Le corbeau noir a été  enfermé dans une caverne secrète pour des siècles et des siècles par un des légendaires mages. Grâce aux changements climatiques, il a réussi à se libérer et il revient prendre sa revanche .  Cinq élèves choisis aléatoirement devront affronter le corbeau dans un combat de l’esprit.\r\n\r\nLe Maître du jeu devra donner 24 heures à ces élèves pour accomplir une tâche.\r\nIls devront accomplir la tâche correctement dans le délai prescrit.  S'ils réussissent, ils gagnent 300 Xp et 200 PO.  Sinon, ils perdent 200 XP et 1 coeur" },
        {"Le grand sacrifice", "Un démon affamé vient attaquer votre village et s'empare de tous les membres de l'équipe d'un élève choisi aléatroirement. Ce brave guerrier devra vaincre le démon ou subir  les conséquence vraiment graves.  Oh brave guerrier!  Il faut sauver tes coéquipiers avant qu'ils finissent dans le ventre du monstre.   L'élève choisi aléatoirement doit répondre à une question choisie par le Maître du jeu. S'il réussit, il gagne 200 Xp, 3 cristaux et 50 PO et les membres de son équipe gagnent 100XP et 20PO.  Sinon, il perd 200 XP et 20PO  et son groupe perd 100XP." },
        {"Maître Chaton", "Vous êtes maudits par la vieille dame aux chats! Tout le monde doit terminer ses phrases avec un « miaou ».  S'ils le font bien, ils gagnent 300 Xp.  Sinon, ils perdent 200 XP" },
        {"Moment de tranquillité", "Certains guérriers ont été réduits au silence! Les guerriers d'un groupe choisi aléatoirment doivent garder le silence pour toute la durée du cours. S'ils le font bien, ils gagnent 300 Xp.  Sinon, ils perdent 500 XP" },
        {"Perte d’énergie", "Vous avez mal dormi et vous vous sentez fatigués. Tout le monde perd 3 cristaux." },
        {"Potion de polymorphie", "Vous avez bu une potion dont vous auriez dû lire l’étiquette avant! Un joueur sélectionné aléatoirement est devenu un lion et le groupe doit l’appeler « Roi lion » et il doit rugir avant de parler pour toute la durée du cours. S'il le fait bien, il gagne 300 Xp.  Sinon, il perd 200 XP" },
        {"Savoir nécessaire", "Gertrude, une enseignante maléfique, tourmente les élèves. Chaque membre d’une équipe choisie aléatoirement doit répondre à une question.  S'ils réussissent, ils gagnent 200 Xp.  Sinon, ils perdent 2 coeurs." },
        {"Sortilège de Méduse", "Méduse est sortie de sa caverne!!! Cinq élèves choisis aléatoirement sont pétrifiés par le regard de Méduse.  Au signal du Maître du jeu, ces élèves gèlent dans leur position pour une durée de 2 minutes. S'ils le font bien, ils gagnent 300 Xp.  Sinon, ils perdent 1 coeur." },
        {"Tremblement de terre", "La terre tremble sous vos pieds. Tout le monde perd 2 coeurs." },
        {"Un cadeau du maître du jeu", "Aujourd’hui, c’est ton jour de chance! Un joueur sélectionné aléatoirement gagne 1000 XP" },
        {"Un cadeau POUR le Maître du jeu", "Vous trouvez que le Maître du jeu en fait beaucoup pour vous et vous voulez lui faire un petit cadeau. Un joueur choisi aléatoirement doit faire ce que le Maître du jeu lui dit de faire pour toute la période. S'il le fait bien, il gagne 400 Xp.  Sinon, ils perdent 200 XP" },
        {"Une vie de pirate", "Oh matelots!  Il vaut mieux que vous soyez de vrais marins, sinon vous marcherez sur la planche! Les membres d’une équipe choisie aléatoirement doivent parler avec un accent de pirate.\r\nTous les joueurs doivent appeler le maître du jeu « Capitaine ».  S'ils le font bien, ils gagnent 200 Xp.  Sinon, ils perdent 100 XP." },
        {"Inspection Royale 1", "Le roi inspectera les troupes. Tout élève qui porte un polo gagne 200 XP" },
        {"Inspection Royale 2", "Le roi inspectera les troupes. Tout élève qui porte une chemise gagne 200 XP" },
        {"Inspection Royale 3", "Le roi inspectera les troupes. Tout élève qui porte une jupe ou des shorts gagne 200 XP" },
        {"Inspection Royale 4", "Le roi inspectera les troupes. Tout élève qui porte des lacets noirs gagne 200 XP" },
        {"Vœux d'anniversaire", "C'est ton anniversaire!!!! Les guerriers dont l'anniversaire est ce moi-ci reçoivent 300 XP" },
        {"Le pouvoir de la connaissance", "Il n'y a rien de meilleur qu'un bon livre de lecture!! Une ou un élève sélectionné au hasard doit parler d'un livre qu'il a lu dernièrement. S'il le fait bien, il gagne 200 Xp.  Sinon, il perd 200 XP." },
        {"Le messager", "Tellement de choses intéressantes se passent autour de nous Une ou un élève sélectionné au hasard doit partager une nouvelle qu'il a lue cette semaine. S'il le fait bien, il gagne 200 Xp.  Sinon, il perd 200 XP." },
        {"L'Énigme du Sphinx", "Votre équipe tombe sur la réserve d'or du Sphinx et il vous met au défi de répondre à une énigme. Le Maître du jeu demande à une équipe sélectionnée aléatoirement de résoudre une énigme. S'ils réussissent, ils gagnent 200 Xp.  Sinon, ils perdent 100 XP." },
        {"Sorcier interrogateur", "La bagarre prend entre deux équipes Deux équipes choisies aléatoirement doivent s'affronter en mode \"\"drill\"\" avec des questions de rapidité. Le groupe gagnant reçoit 500 XP et le perdant perd 100 XP." },
        {"QUE LA FORCE SOIT AVEC VOUS!", "Testez vos connaissances sur Star Wars! Une ou un élève choisi aléatoirement peut choisir de répondre à des questions générales sur Star Wars posées par le maître du jeu. Chaque bonne réponse  = 200 XP\r\nchaque mauvaise réponse = - 1 cœur." },
        {"HOGWARTS VOUS ATTENT!", "Testez vos connaissances sur Harry Potter! Une ou un élève choisi aléatoirement peut choisir de répondre à des questions générales sur Harry Potter posées par le maître du jeu. Chaque bonne réponse  = 200 XP\r\nchaque mauvaise réponse = - 1 cœur." },
        {"L'ÉPREUVE DE BRAD PITT", "Brad Pitt veut tester vos connaissances cinématographiques. Une ou un élève sélectionné au hasard doit nommer 3 films où Brad Pitt apparaît. Chaque bonne réponse  = 200 XP\r\nchaque mauvaise réponse = - 1 cœur." },
        {"L'ÉPREUVE DE QUENTIN TANRANTINO", "Quentin Tarantino veut tester vos connaissances cinématographiques. Une ou un élève sélectionné au hasard doit nommer 3 films dirigés par Quentin Tarantino. Chaque bonne réponse  = 200 XP\r\nchaque mauvaise réponse = - 1 cœur." },
        {"L'ÉPREUVE DE SHAWN MENDES", "Shawn Mendes veut tester vos connaissances musicales. Une ou un élève sélectionné au hasard doit nommer 3 chansons de Shawn Mendes. Chaque bonne réponse  = 200 XP\r\nchaque mauvaise réponse = - 1 cœur." },
        {"L'ÉPREUVE DE ED SHEERAN", "Ed Sheeran veut tester vos connaissances musicales. Une ou un élève sélectionné au hasard doit nommer 3 chansons de Ed Sheeran. Chaque bonne réponse  = 200 XP\r\nchaque mauvaise réponse = - 1 cœur." },
        {"L'ÉPREUVE DE ARIANA GRANDE", "Ariana veut tester vos connaissances musicales. Une ou un élève sélectionné au hasard doit nommer 3 chansons d’Araiana Grande. Chaque bonne réponse  = 200 XP\r\nchaque mauvaise réponse = - 1 cœur." },
        {"L'ÉPREUVE DE RYAN GOSLING", "Ryan Gosling veut tester vos connaissances cinématographiques. Une ou un élève sélectionné au hasard doit nommer 3 films où Ryan Gosling apparaît. Chaque bonne réponse  = 200 XP\r\nchaque mauvaise réponse = - 1 cœur." },
        {"L'ÉPREUVE DE CHRIS HEMSWORTH", "Chris Hemsworth veut tester vos connaissances cinématographiques. Une ou un élève sélectionné au hasard doit nommer 3 films où Chris Hemsworth apparaît. Chaque bonne réponse  = 200 XP chaque mauvaise réponse = - 1 cœur." },
        {"L'ÉPREUVE DE RYAN REYNOLDS", "Ryan Reynolds veut tester vos connaissances cinématographiques. Une ou un élève sélectionné au hasard doit nommer 3 films où Ryan Reynolds apparaît. Chaque bonne réponse  = 200 XP\r\nchaque mauvaise réponse = - 1 cœur." },
        {"L'ENTRAÎNEMENT DU NINJA", "Vous vous entraînez intensément pour devenir plus forts! Aujourd’hui, tout le monde reçoit 500 XP." },
        {"PILE OU FACE?", "Êtes-vous prêts au combat de la chance? Les élèves se placent aux deux côtés de la classe.  Un côté représente Pile et l’autre côté Face.  Le Maître du jeu lance une pièce d’argent.  Le côté qui gagne reçoit 300XP.  Celui qui perds se fait enlever 1 coeur." },
        {"COUP DE DÉ #1", "Vous voulez tous gagner!! Les élèves se placent aux quatre coins de la classe.  (chaque coin représente 1,2,3 ou 4 respectivement).  Le Maître du jeu lance le dé.  Si le dé tombe sur 6, tous les élèves gagnent 200XP.  S’il tombe sur le 5, tous les élèves perdent 2 coeurs.  S’il tombe sur 1,2,3 ou 4, les élèves dans le coin correspondant à ce nombre gagnent 500XP." },
        {"COUP DE DÉ #2", "Vous voulez tous gagner!! Les élèves se placent aux quatre coins de la classe.  (chaque coin représente 1,2,3 ou 4 respectivement).  Le Maître du jeu lance le dé.  Si le dé tombe sur 6, tous les élèves gagnent 200XP.  S’il tombe sur le 5, tous les élèves perdent 2 coeurs.  S’il tombe sur 1,2,3 ou 4, les élèves dans le coin correspondant à ce nombre gagnent 500XP." },
        {"COUP DE DÉ #3", "Vous voulez tous gagner!! Les élèves se placent aux quatre coins de la classe.  (chaque coin représente 1,2,3 ou 4 respectivement).  Le Maître du jeu lance le dé.  Si le dé tombe sur 6, tous les élèves gagnent 200XP.  S’il tombe sur le 5, tous les élèves perdent 2 coeurs.  S’il tombe sur 1,2,3 ou 4, les élèves dans le coin correspondant à ce nombre gagnent 500XP." },
        {"COUP DE DÉ #4", "Vous voulez tous gagner!! Les élèves se placent aux quatre coins de la classe.  (chaque coin représente 1,2,3 ou 4 respectivement).  Le Maître du jeu lance le dé.  Si le dé tombe sur 6, tous les élèves gagnent 200XP.  S’il tombe sur le 5, tous les élèves perdent 2 coeurs.  S’il tombe sur 1,2,3 ou 4, les élèves dans le coin correspondant à ce nombre gagnent 500XP." },
        {"LE LUTIN FARFADET", "Une ou un élève de la classe a capturé une créature humanoïde de petite taille avec une barbe, coiffée d'un chapeau et vêtue de rouge et de vert. Selon le chaman du village, lorsque le Farfadet se fait capturer, il peut exaucer trois vœux en échange de sa libération. Trois élèves sélectionnés au hasard ont droit à un vœu chacun. Voici les vœux possibles : régénérer tous ses coeurs, régénérer tous ses crystaux, gagner 100 pièces d’or ou obtenir 200 XP." },
        {"UN VISITEUR FOU", "Vos connaissances en géographie vont déterminer votre sort. Une ou un élève sélectionné au hasard doit deviner la capitale d’un pays choisi par le maître du jeu. S'il le fait bien, il gagne 200 Xp.  Sinon, il perd 1 coeur." },
        {"FIGHT CLUB", "La première règle de Fight Club est que personne ne parle de Fight Club! Cinq élèves choisis aléatoirement doivent être calmes et tranquilles pendant toute la période. Le plus tranquille recevra 400 XP. Le moins tranquille perdra 200 XP." },
        {"NI OUI, NI NON", "Il faut penser avant de parler. Les élèves gagnent 100 XP chaque fois que le Maître du jeu dit «oui» ou «non»." },
        {"LE DÉ MAGIQUE!", "Le Maître du jeu lance le dé.  La conséquence dépend du numéro lancé : Si le dé tombe sur 1: Le joueur de chaque équipe avec le plus de XP perd 100XP.\r\nSi le dé tombe sur 2: Un joueur de chaque équipe doit se sacrifier et faire 10 push-ups.  (Si personne ne prend le défi, toute l’équipe perd 10HP.)\r\nSi le dé tombe sur 3: Un joueur de chaque équipe joue à roche papier ciseaux contre le joueur des autres équipes. l'équipe du gagant reçoit 200XP.\r\nSi le dé tombe sur 4: Le Maître du jeu relance le dé.  Si ça tombe sur un nombre pair, tout le monde gagne 200XP.  Si ça tombe sur un nombre impair, tout le monde perd 1 coeur.\r\nSi le dé tombe sur 5: Tous les membres d’une équipe choisie aléatoirement reçoivent un morceau de chocolat.\r\nSi le dé tombe sur 6: Un élève choisi aléatoirement gagne 1000XP." },
        {"UN P’TIT COUP DE POUCE!", "Pour aider les plus faibles, pour aider les plus forts, aucune importance, au combat, seul le courage compte!! Les membres d’une équipe choisie aléatoirement reçoivent 500 XP et 500 PO." },
        {"Compliment véritable", "Ah qu'on se sent bien quant on est apprécié Un élève choisi aléatoirement doit choisir un autre élève et lui donner 5 compliments.  Si le Maître du jeu juge que les compliments sont sérieux et bien livrés, les deux élèves reçoivent 200XP" },
        {"Buffet de la reine des araignées", "La reine des araignées vous a piègé dans sa toile. Un élève par équipe (choisi aléatoirement) perd 1 cœur." },
        {"Chute dorée", "Un groupe tombe du haut d'une montagne mais, par chance, la toile d'un pavillon ralenti la chute. Et chance double, le pavillon contient les trésors des Maîtres!! Un groupe choisi aléatoirement perd 2 cœurs mais gagne 100 PO" },
        {"Dovakine", "Dovakine, l'enfant du dragon, décide de prendre un apprenti parmi les meilleurs guerriers. Un élève choisi aléatoirement gagne 750 XP" },
        {"Fusion", "Deux personnes valent mieux qu'une! Lors de la prochaine évaluation, un joueur choisi aléatoirement pourra choisir un coéquipier avec qui se joindre pour résoudre une des questions." },
        {"Genkidama", "Un guerrier a besoin de votre énergie. Toute la classe (sauf un guerrier choisi aléatoirement) perd tous ses cristaux mais gagne 600 XP.  Le guerrier reçoit le niveau maximal de cristaux et gagne 100 XP." },
        {"La journée de l'humour", "Il faut toujours réussir à rire dans la vie!! 3 joueurs, choisis aléatoirement, doivent raconter une blague.  Le joueur qui a raconté la meilleure blague remporte 400 XP" },
        {"Le chasseur de sorcière", "Van Helsing, le chasseur de sorcière tant réputé est là!! Un joueur, choisi aléatoirement, perd 1 cœur" },
        {"Le roi de la montagne", "Le roi de la montagne est prêt au combat! Cinq joueurs choisis aléatroirement doivent répondre à une questions posée par le Maître du jeu.  S'ils réussissent, ils gagnent 500 Xp.  Sinon, ils perdent 1 coeur." },
        {"Les barbares de l'élite", "Deux barbares attaquent les plus puissants combattants. Les deux guerriers qui ont le plus de XPs perdent 1 cœur" },
        {"Iluminati", "Vous avez trouvé la base secrète de la secte des illuminatis.  Ils vous paient pour que vous ne dévoiliez pas leur secret. Tout le monde gagne 200 PO" },
        {"Mister Muscle", "Un joueur de chaque équipe doit faire des push-ups. Les push-ups doivent être bien faits!!  L'équipe du joueur qui fait le plus de push-ups gagne 400 XP" },
        {"Médecin à la rescousse", "Le roi est mourant et il faut le soigner. Seul une pétale de la fleur de renaissance peut le sauver.  Mais cette fleur est très rare!! Deux élèves choisis aléatoirement réussissent à trouver la fleur de renaissance qui pourra guérir le roi. Pour leur exploit, ils reçoivent 500 XP et 2 cristaux." },
        {"Réunion de famille", "Les familles se réunissent pour un grand dîner. Tout le monde doit s'appeler par son nom de famille.  S'ils le font bien, ils gagnent 200 Xp.  Sinon, ils perdent 200 XP." },
        {"C'est ta fête", "Tous les élèves doivent calculer le montant de jours avant leur fête.  (pas après).  Les cinq élèves qui vont célébrer leur fête les premiers reçoivent des XP basés sur un barême dégressif.  \r\nVoici l'équation pour le calcul:\r\n(XP) = 1500 - 20j  \r\n(où j = nombre de jours qui séparent aujourd'hui du jour de fête (à venir)" },
        {"Le grand menteur", "Le Maître du jeu doit dire 2 vérités et un mensonge et les élèves doivent trouver le mensonge.  Ceux qui ont trouvé le mensonge reçoivent 800 XP." },};
    }

    // Update is called once per frame
    public void Update()  //checks if wheel is still spinning 
    {
       if(isSpinning == true)
        {
            transform.Rotate(0,0,spinSpeed, Space.World);
            spinSpeed -= decelerationSpeed;
        }
        if (spinSpeed <= 0 && count < 1)
        {
            spinSpeed = 0;
            isSpinning = false;
            displayPrize(determineSection());
            count += 1;
        }
    }

    public void spinWheel() //function that spins the wheel
    {
        awardPopUp.SetActive(false);
        spinSpeed = Random.Range(2.000f, 5.000f);
        decelerationSpeed = Random.Range(0.003f, 0.009f);
        isSpinning = true;
        count = 0;
    }

    public int determineSection() //function that determines the section the wheel landed on
    {

        int section = Random.Range(0, numberOfPrizes + 1);

        Debug.Log("Wheel landed on section: " + (section));
        return section;
    }

    public void displayPrize(int section) //function that displays the prize that was landed
    {
        // Display the prize and show the popup
        prizeText.text = prizes[section, 1];
        prizeTitleText.text = prizes[section, 0];
        awardPopUp.SetActive(true);
    }
}
