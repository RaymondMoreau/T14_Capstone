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
        { { "50/50", "Un homme approche un d�entre vous qui a �t� choisi al�atoirement avec une potion et lui dit:  �Si tu bois cette potion, tu peux soit doubler ta puissance ou la diviser� S'il accepte le d�fi, il doit lancer une pi�ce dans les airs et si elle atterrit sur pile, tout son groupe  gagne 400 XP.  Si la pi�ce atterrit sur  face, tout le groupe perd  la moiti� de leur coeurs." }, 
        {"Ah\"NOM\"!", "Le Ma�tre du jeu a bu une potion magique par erreur, et cela le rend un peu confus aujourd�hui... Chaque fois que le Ma�tre du jeu utilise le nom d'un �l�ve, tous les joueurs gagnent 100 XP" },
        {"Bataille de champions", "Vous croisez un chevalier qui, par jalousie, attaque le plus fort d�entre vous. Le joueur avec le plus de X�P�perd 2 coeurs" },
        {"B�n�diction ou mal�diction? ", "Vous trouvez une lame maudite� Un joueur s�lectionn� al�atoirement perd 5 cristaux mais gagne 300 XP" },
        {"Bienvenue dans la jungle!", "Le monde de Classcraft est rempli de surprises! Un joueur al�atoire par �quipe�perd un coeur" },
        {"Changement de fortune", "Vous avez de la chance et �tes destin�s � de bonnes choses. Une �quipe choisie al�atoirement gagne 300 XP" },
        {"Choisir de prosp�rer", "Un extraterrestre atterrit sur la terre et demande � parler � votre leader. Chaque �quipe �lit un co�quipier qui gagnera 300 XP" },
        {"Clou�s sur vos si�ges", "Certains gu�rriers d�cident d�observer les choses de loin. Les guerriers d'un groupe choisi al�atoirment�doivent rester assis jusqu�au son de la cloche. S'ils le font bien, ils gagnent 500 Xp.  Sinon, ils perdent 300 XP" },
        {"Des bonnes mani�res", "Vous vous exercez pour la visite du roi. Tout le monde doit s�adresser aux autres en utilisant ��votre majest頻. S'ils le font bien, ils gagnent 300 Xp.  Sinon, ils perdent 200 XP" },
        {"�nergie abondante", "Vous trouvez une potion myst�rieuse et la donnez au plus faible. Le joueur avec le moins de cristaux dans chaque �quipe gagne 3 cristaux." },
        {"Force brutale", "Certains gu�rriers s�exercent tous ensemble. Les guerriers d'un groupe choisi al�atoirment doivent rester debout pout toute la dur�e du cours. S'ils le font bien, ils gagnent 500 Xp.  Sinon, ils perdent 300 XP" },
        {"L�abondance r�gne", "Vos co�quipiers sont g�n�reux et vous aident. Le joueur avec le moins de XP dans chaque �quipe gagne 300 XP" },
        {"La Grande Niveleuse", "Elle se prononce et condamne� Tout le monde voient leurs coeurs tomber (ou monter) � 3." },
        {"L'attaque du m�chant roi", "Un m�chant Roi, qui vit dans un pays lointain, a perdu l�acc�s � l�internet royal et donc s�ennuie � ne rien faire.� Il d�cide alors de partir conqu�rir des pays avoisinants qu�il va choisir de fa�on al�atoire.  Il vous attaque et les gu�rriers de l��quipe choisie al�atoirement d�cident de se sacrifier au combat pour permettre au restant de la classe de vaincre ce m�chant roi.  Ils perdent 2 coeurs mais re�oivent 300Xp pour leur bravoure." },
        {"Le pire Karaok� EVER!!", "Amusons-nous un peu� Le joueur avec le moins de XP choisit une chanson karaoke.  Le ma�tre du jeu a 24h pour pratiquer la chanson.  Pour avoir fait ce choix judicieux, le joueur re�oit 300 XP." },
        {"Le chemin de la croissance", "Vous voulez r�ussir � affronter tous les obstacles devant vous sur la route du succ�s.  Vous devez vous entra�ner. Cinq �l�ves choisis al�atoirement doivent r�pondre � une question du Ma�tre du jeu. S'ils r�ussissent, ils gagnent 200 Xp et 100 PO.  Sinon, ils perdent 100 XP et 80PO" },
        {"Le corbeau noir", "Le corbeau noir a �t� enferm� dans une caverne secr�te pour des si�cles et des si�cles par un des l�gendaires mages. Gr�ce aux changements climatiques, il a r�ussi � se lib�rer et il revient prendre sa revanche .  Cinq �l�ves choisis al�atoirement devront affronter le corbeau dans un combat de l�esprit.\r\n\r\nLe Ma�tre du jeu devra donner 24 heures � ces �l�ves pour accomplir une t�che.\r\nIls devront accomplir la t�che correctement dans le d�lai prescrit.  S'ils r�ussissent, ils gagnent 300 Xp et 200 PO.  Sinon, ils perdent 200 XP et 1 coeur" },
        {"Le grand sacrifice", "Un d�mon affam� vient attaquer votre village et s'empare de tous les membres de l'�quipe d'un �l�ve choisi al�atroirement. Ce brave guerrier devra vaincre le d�mon ou subir� les cons�quence vraiment graves.� Oh brave guerrier!� Il faut sauver tes co�quipiers avant qu'ils finissent dans le ventre du monstre.�  L'�l�ve choisi al�atoirement doit r�pondre � une question choisie par le Ma�tre du jeu. S'il r�ussit, il gagne 200 Xp, 3 cristaux et 50 PO et les membres de son �quipe gagnent 100XP et 20PO.  Sinon, il perd 200 XP et 20PO  et son groupe perd 100XP." },
        {"Ma�tre Chaton", "Vous �tes maudits par la vieille dame aux chats! Tout le monde doit terminer ses phrases avec un ��miaou��.  S'ils le font bien, ils gagnent 300 Xp.  Sinon, ils perdent 200 XP" },
        {"Moment de tranquillit�", "Certains gu�rriers ont �t� r�duits au silence! Les guerriers d'un groupe choisi al�atoirment doivent garder le silence pour toute la dur�e du cours. S'ils le font bien, ils gagnent 300 Xp.  Sinon, ils perdent 500 XP" },
        {"Perte d��nergie", "Vous avez mal dormi et vous vous sentez fatigu�s. Tout le monde perd 3 cristaux." },
        {"Potion de polymorphie", "Vous avez bu une potion dont vous auriez d� lire l��tiquette avant! Un joueur s�lectionn� al�atoirement est devenu un lion et le groupe doit l�appeler ��Roi lion�� et il doit rugir avant de parler pour toute la dur�e du cours. S'il le fait bien, il gagne 300 Xp.  Sinon, il perd 200 XP" },
        {"Savoir n�cessaire", "Gertrude, une enseignante mal�fique, tourmente les �l�ves. Chaque membre d�une �quipe choisie al�atoirement doit r�pondre � une question.  S'ils r�ussissent, ils gagnent 200 Xp.  Sinon, ils perdent 2 coeurs." },
        {"Sortil�ge de M�duse", "M�duse est sortie de sa caverne!!! Cinq �l�ves choisis al�atoirement sont p�trifi�s par le regard de M�duse.� Au signal du Ma�tre du jeu, ces �l�ves g�lent dans leur position pour une dur�e de 2 minutes. S'ils le font bien, ils gagnent 300 Xp.  Sinon, ils perdent 1 coeur." },
        {"Tremblement de terre", "La terre tremble sous vos pieds. Tout le monde perd 2 coeurs." },
        {"Un cadeau du ma�tre du jeu", "Aujourd�hui, c�est ton jour de chance! Un joueur s�lectionn� al�atoirement gagne 1000 XP" },
        {"Un cadeau POUR le Ma�tre du jeu", "Vous trouvez que le Ma�tre du jeu en fait beaucoup pour vous et vous voulez lui faire un petit cadeau. Un joueur choisi al�atoirement doit faire ce que le Ma�tre du jeu lui dit de faire pour toute la p�riode. S'il le fait bien, il gagne 400 Xp.  Sinon, ils perdent 200 XP" },
        {"Une vie de pirate", "Oh matelots!  Il vaut mieux que vous soyez de vrais marins, sinon vous marcherez sur la planche! Les membres d�une �quipe choisie al�atoirement doivent parler avec un accent de pirate.\r\nTous les joueurs doivent appeler le ma�tre du jeu ��Capitaine��.  S'ils le font bien, ils gagnent 200 Xp.  Sinon, ils perdent 100 XP." },
        {"Inspection Royale 1", "Le roi inspectera les troupes. Tout �l�ve qui porte un polo gagne 200 XP" },
        {"Inspection Royale 2", "Le roi inspectera les troupes. Tout �l�ve qui porte une chemise gagne 200 XP" },
        {"Inspection Royale 3", "Le roi inspectera les troupes. Tout �l�ve qui porte une jupe ou des shorts gagne 200 XP" },
        {"Inspection Royale 4", "Le roi inspectera les troupes. Tout �l�ve qui porte des lacets noirs gagne 200 XP" },
        {"V�ux d'anniversaire", "C'est ton anniversaire!!!! Les guerriers dont l'anniversaire est ce moi-ci re�oivent 300 XP" },
        {"Le pouvoir de la connaissance", "Il n'y a rien de meilleur qu'un bon livre de lecture!! Une ou un �l�ve s�lectionn� au hasard doit parler d'un livre qu'il a lu derni�rement. S'il le fait bien, il gagne 200 Xp.  Sinon, il perd 200 XP." },
        {"Le messager", "Tellement de choses int�ressantes se passent autour de nous Une ou un �l�ve s�lectionn� au hasard doit partager une nouvelle qu'il a lue cette semaine. S'il le fait bien, il gagne 200 Xp.  Sinon, il perd 200 XP." },
        {"L'�nigme du Sphinx", "Votre �quipe tombe sur la r�serve d'or du Sphinx et il vous met au d�fi de r�pondre � une �nigme. Le Ma�tre du jeu demande � une �quipe s�lectionn�e al�atoirement de r�soudre une �nigme. S'ils r�ussissent, ils gagnent 200 Xp.  Sinon, ils perdent 100 XP." },
        {"Sorcier interrogateur", "La bagarre prend entre deux �quipes Deux �quipes choisies al�atoirement doivent s'affronter en mode \"\"drill\"\" avec des questions de rapidit�. Le groupe gagnant re�oit 500 XP et le perdant perd 100 XP." },
        {"QUE LA FORCE SOIT AVEC VOUS!", "Testez vos connaissances sur Star Wars! Une ou un �l�ve choisi al�atoirement peut choisir de r�pondre � des questions g�n�rales sur Star Wars pos�es par le ma�tre du jeu. Chaque bonne r�ponse  = 200 XP\r\nchaque mauvaise r�ponse = - 1 c�ur." },
        {"HOGWARTS VOUS ATTENT!", "Testez vos connaissances sur�Harry Potter! Une ou un �l�ve choisi al�atoirement peut choisir de�r�pondre � des questions g�n�rales sur�Harry Potter�pos�es par le ma�tre du jeu. Chaque bonne r�ponse  = 200 XP\r\nchaque mauvaise r�ponse = - 1 c�ur." },
        {"L'�PREUVE DE BRAD PITT", "Brad Pitt veut tester vos connaissances cin�matographiques. Une ou un �l�ve s�lectionn� au hasard doit nommer 3 films o� Brad Pitt appara�t. Chaque bonne r�ponse  = 200 XP\r\nchaque mauvaise r�ponse = - 1 c�ur." },
        {"L'�PREUVE DE QUENTIN TANRANTINO", "Quentin Tarantino veut tester vos connaissances cin�matographiques. Une ou un �l�ve s�lectionn� au hasard doit nommer 3 films dirig�s par Quentin Tarantino. Chaque bonne r�ponse  = 200 XP\r\nchaque mauvaise r�ponse = - 1 c�ur." },
        {"L'�PREUVE DE SHAWN MENDES", "Shawn Mendes veut tester vos connaissances musicales. Une ou un �l�ve s�lectionn� au hasard doit nommer 3 chansons de Shawn Mendes. Chaque bonne r�ponse  = 200 XP\r\nchaque mauvaise r�ponse = - 1 c�ur." },
        {"L'�PREUVE DE ED SHEERAN", "Ed Sheeran veut tester vos connaissances musicales. Une ou un �l�ve s�lectionn� au hasard doit nommer 3 chansons de Ed Sheeran. Chaque bonne r�ponse  = 200 XP\r\nchaque mauvaise r�ponse = - 1 c�ur." },
        {"L'�PREUVE DE ARIANA GRANDE", "Ariana veut tester vos connaissances musicales. Une ou un �l�ve s�lectionn� au hasard doit nommer 3 chansons d�Araiana Grande. Chaque bonne r�ponse  = 200 XP\r\nchaque mauvaise r�ponse = - 1 c�ur." },
        {"L'�PREUVE DE RYAN GOSLING", "Ryan Gosling veut tester vos connaissances cin�matographiques. Une ou un �l�ve s�lectionn� au hasard doit nommer 3 films o� Ryan Gosling appara�t. Chaque bonne r�ponse  = 200 XP\r\nchaque mauvaise r�ponse = - 1 c�ur." },
        {"L'�PREUVE DE CHRIS HEMSWORTH", "Chris Hemsworth veut tester vos connaissances cin�matographiques. Une ou un �l�ve s�lectionn� au hasard doit nommer 3 films o� Chris Hemsworth appara�t. Chaque bonne r�ponse  = 200 XP chaque mauvaise r�ponse = - 1 c�ur." },
        {"L'�PREUVE DE RYAN REYNOLDS", "Ryan Reynolds veut tester vos connaissances cin�matographiques. Une ou un �l�ve s�lectionn� au hasard doit nommer 3 films o� Ryan Reynolds appara�t. Chaque bonne r�ponse  = 200 XP\r\nchaque mauvaise r�ponse = - 1 c�ur." },
        {"L'ENTRA�NEMENT DU NINJA", "Vous vous entra�nez intens�ment pour devenir plus forts! Aujourd�hui, tout le monde re�oit 500 XP." },
        {"PILE OU FACE?", "�tes-vous pr�ts au combat de la chance? Les �l�ves se placent aux deux c�t�s de la classe.  Un c�t� repr�sente Pile et l�autre c�t� Face.  Le Ma�tre du jeu lance une pi�ce d�argent.  Le c�t� qui gagne re�oit 300XP.  Celui qui perds se fait enlever 1 coeur." },
        {"COUP DE D� #1", "Vous voulez tous gagner!! Les �l�ves se placent aux quatre coins de la classe.  (chaque coin repr�sente 1,2,3 ou 4 respectivement).  Le Ma�tre du jeu lance le d�.  Si le d� tombe sur 6, tous les �l�ves gagnent 200XP.  S�il tombe sur le 5, tous les �l�ves perdent 2 coeurs.  S�il tombe sur 1,2,3 ou 4, les �l�ves dans le coin correspondant � ce nombre gagnent 500XP." },
        {"COUP DE D� #2", "Vous voulez tous gagner!! Les �l�ves se placent aux quatre coins de la classe.  (chaque coin repr�sente 1,2,3 ou 4 respectivement).  Le Ma�tre du jeu lance le d�.  Si le d� tombe sur 6, tous les �l�ves gagnent 200XP.  S�il tombe sur le 5, tous les �l�ves perdent 2 coeurs.  S�il tombe sur 1,2,3 ou 4, les �l�ves dans le coin correspondant � ce nombre gagnent 500XP." },
        {"COUP DE D� #3", "Vous voulez tous gagner!! Les �l�ves se placent aux quatre coins de la classe.  (chaque coin repr�sente 1,2,3 ou 4 respectivement).  Le Ma�tre du jeu lance le d�.  Si le d� tombe sur 6, tous les �l�ves gagnent 200XP.  S�il tombe sur le 5, tous les �l�ves perdent 2 coeurs.  S�il tombe sur 1,2,3 ou 4, les �l�ves dans le coin correspondant � ce nombre gagnent 500XP." },
        {"COUP DE D� #4", "Vous voulez tous gagner!! Les �l�ves se placent aux quatre coins de la classe.  (chaque coin repr�sente 1,2,3 ou 4 respectivement).  Le Ma�tre du jeu lance le d�.  Si le d� tombe sur 6, tous les �l�ves gagnent 200XP.  S�il tombe sur le 5, tous les �l�ves perdent 2 coeurs.  S�il tombe sur 1,2,3 ou 4, les �l�ves dans le coin correspondant � ce nombre gagnent 500XP." },
        {"LE LUTIN FARFADET", "Une ou un �l�ve de la classe a captur� une cr�ature humano�de de petite taille avec une barbe, coiff�e d'un chapeau et v�tue de rouge et�de vert. Selon le chaman du village, lorsque le Farfadet se fait capturer, il peut exaucer trois v�ux en �change de sa lib�ration. Trois �l�ves s�lectionn�s au hasard ont droit � un v�u chacun. Voici les v�ux possibles : r�g�n�rer tous ses coeurs, r�g�n�rer tous ses crystaux, gagner 100 pi�ces d�or ou obtenir 200 XP." },
        {"UN VISITEUR FOU", "Vos connaissances en g�ographie vont d�terminer votre sort. Une ou un �l�ve s�lectionn�au hasard doit�deviner la capitale d�un pays choisi par le ma�tre du jeu. S'il le fait bien, il gagne 200 Xp.  Sinon, il perd 1 coeur." },
        {"FIGHT CLUB", "La premi�re r�gle de�Fight Club est que personne ne parle de Fight Club! Cinq �l�ves choisis al�atoirement doivent �tre calmes et tranquilles pendant toute la p�riode. Le plus tranquille recevra 400 XP. Le moins tranquille perdra 200 XP." },
        {"NI OUI, NI NON", "Il faut penser avant de parler. Les��l�ves gagnent 100 XP chaque fois que le Ma�tre du jeu dit �oui� ou �non�." },
        {"LE D� MAGIQUE!", "Le Ma�tre du jeu lance le d�.  La cons�quence d�pend du num�ro lanc�: Si le d� tombe sur 1: Le joueur de chaque �quipe avec le plus de XP perd 100XP.\r\nSi le d� tombe sur 2: Un joueur de chaque �quipe doit se sacrifier et faire 10 push-ups.  (Si personne ne prend le d�fi, toute l��quipe perd 10HP.)\r\nSi le d� tombe sur 3: Un joueur de chaque �quipe joue � roche papier ciseaux contre le joueur des autres �quipes. l'�quipe du gagant re�oit 200XP.\r\nSi le d� tombe sur 4: Le Ma�tre du jeu relance le d�.  Si �a tombe sur un nombre pair, tout le monde gagne 200XP.  Si �a tombe sur un nombre impair, tout le monde perd 1 coeur.\r\nSi le d� tombe sur 5: Tous les membres d�une �quipe choisie al�atoirement re�oivent un morceau de chocolat.\r\nSi le d� tombe sur 6: Un �l�ve choisi al�atoirement gagne 1000XP." },
        {"UN P�TIT COUP DE POUCE!", "Pour aider les plus faibles, pour aider les plus forts, aucune importance, au combat, seul le courage compte!! Les membres d�une �quipe choisie al�atoirement re�oivent 500 XP et 500 PO." },
        {"Compliment v�ritable", "Ah qu'on se sent bien quant on est appr�ci� Un �l�ve choisi al�atoirement doit choisir un autre �l�ve et lui donner 5 compliments.  Si le Ma�tre du jeu juge que les compliments sont s�rieux et bien livr�s, les deux �l�ves re�oivent 200XP" },
        {"Buffet de la reine des araign�es", "La reine des araign�es vous a pi�g� dans sa toile. Un �l�ve par �quipe (choisi al�atoirement) perd 1 c�ur." },
        {"Chute dor�e", "Un groupe tombe du haut d'une montagne mais, par chance, la toile d'un pavillon ralenti la chute. Et chance double, le pavillon contient les tr�sors des Ma�tres!! Un groupe choisi al�atoirement perd 2 c�urs mais gagne 100 PO" },
        {"Dovakine", "Dovakine, l'enfant du dragon, d�cide de prendre un apprenti parmi les meilleurs guerriers. Un �l�ve choisi al�atoirement gagne 750 XP" },
        {"Fusion", "Deux personnes valent mieux qu'une! Lors de la prochaine �valuation, un joueur choisi al�atoirement pourra choisir un co�quipier avec qui se joindre pour r�soudre une des questions." },
        {"Genkidama", "Un guerrier a besoin de votre �nergie. Toute la classe (sauf un guerrier choisi al�atoirement) perd tous ses cristaux mais gagne 600 XP.  Le guerrier re�oit le niveau maximal de cristaux et gagne 100 XP." },
        {"La journ�e de l'humour", "Il faut toujours r�ussir � rire dans la vie!! 3 joueurs, choisis al�atoirement, doivent raconter une blague.  Le joueur qui a racont� la meilleure blague remporte 400 XP" },
        {"Le chasseur de sorci�re", "Van Helsing, le chasseur de sorci�re tant r�put� est l�!! Un joueur, choisi al�atoirement, perd 1 c�ur" },
        {"Le roi de la montagne", "Le roi de la montagne est pr�t au combat! Cinq joueurs choisis al�atroirement doivent r�pondre � une questions pos�e par le Ma�tre du jeu.  S'ils r�ussissent, ils gagnent 500 Xp.  Sinon, ils perdent 1 coeur." },
        {"Les barbares de l'�lite", "Deux barbares attaquent les plus puissants combattants. Les deux guerriers qui ont le plus de XPs perdent 1 c�ur" },
        {"Iluminati", "Vous avez trouv� la base secr�te de la secte des illuminatis.  Ils vous paient pour que vous ne d�voiliez pas leur secret. Tout le monde gagne 200 PO" },
        {"Mister Muscle", "Un joueur de chaque �quipe doit faire des push-ups. Les push-ups doivent �tre bien faits!!  L'�quipe du joueur qui fait le plus de push-ups gagne 400 XP" },
        {"M�decin � la rescousse", "Le roi est mourant et il faut le soigner. Seul une p�tale de la fleur de renaissance peut le sauver.  Mais cette fleur est tr�s rare!! Deux �l�ves choisis al�atoirement r�ussissent � trouver la fleur de renaissance qui pourra gu�rir le roi. Pour leur exploit, ils re�oivent 500 XP et 2 cristaux." },
        {"R�union de famille", "Les familles se r�unissent pour un grand d�ner. Tout le monde doit s'appeler par son nom de famille.  S'ils le font bien, ils gagnent 200 Xp.  Sinon, ils perdent 200 XP." },
        {"C'est ta f�te", "Tous les �l�ves doivent calculer le montant de jours avant leur f�te.  (pas apr�s).  Les cinq �l�ves qui vont c�l�brer leur f�te les premiers re�oivent des XP bas�s sur un bar�me d�gressif.  \r\nVoici l'�quation pour le calcul:\r\n(XP) = 1500 - 20j  \r\n(o� j = nombre de jours qui s�parent aujourd'hui du jour de f�te (� venir)" },
        {"Le grand menteur", "Le Ma�tre du jeu doit dire 2 v�rit�s et un mensonge et les �l�ves doivent trouver le mensonge.  Ceux qui ont trouv� le mensonge re�oivent 800 XP." },};
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
