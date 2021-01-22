using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Info_Fondamentale
{
    class Program
    {
        /// <summary>
        /// Méthode qui intérragit avec l'utilisateur et appelle les autres méthodes
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //on demande le nombre de sommets à l'utilisateur
            Console.WriteLine("Veuillez entrer le nombre de sommets");
            int n = Convert.ToInt32(Console.ReadLine());
            
            //on remplit la matrice des arrêtes de façon aléatoire
            int[,] matrice = RemplissageAleatoire(n);
            AfficherMatrice(matrice);

            //essai de 3-coloration
            //int[,] matrice = { { 1, 0, 1, 1, 1 }, { 0, 1, 1, 1, 0 }, { 1, 1, 1, 1, 0 }, { 1, 1, 1, 1, 1 }, { 1, 0, 0, 1, 1 } };

            //essai pentagone
            //int[,] matrice = { { 1, 1, 0, 0, 1 }, { 1, 1, 1, 0, 0 }, { 0, 1, 1, 1, 0 }, { 0, 0, 1, 1, 1 }, { 1, 0, 0, 1, 1 } };

            //essai carré
            //int[,] matrice = { { 1, 1, 0, 1 }, { 1, 1, 1, 0 }, { 0, 1, 1, 1 }, { 1, 0, 1, 1 } };

            //essai triangle
            //int[,] matrice = { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
            //AfficherMatrice(matrice);

            //On créé une matrice dont la 1ere ligne contient le degré du sommet de la 2eme ligne
            int total = 0;
            int[,] degré = new int[2, matrice.GetLength(1)];
            for (int ligne = 0; ligne < degré.GetLength(1); ligne++)
            {
                for (int colonne = 0; colonne < degré.GetLength(1); colonne++)
                {
                    degré[0, ligne] += matrice[ligne, colonne];
                }
                degré[1, ligne] = ligne;                
                degré[0, ligne]--; //On retire 1 à la somme des liaisons car un sommet est relié à lui-même
                total += degré[0, ligne];
            }
            Console.WriteLine("total d'arrête : " + total/2);
            //AfficherMatrice(degré);

            //on tri la matrice par degré décroissant
            degré = Tri(degré);
            //AfficherMatrice(degré);

            //on définit la couleur du sommet de plus haut degré sur rouge, on fait donc commencer le programme de coloration à la 2eme colonne du tableau
            int a = 1;

            Console.WriteLine("2-Coloration : ");
            int[] coloration2 = new int[degré.GetLength(1)]; //On définit le tableau qui prendra le résultat de la coloration
            coloration2[0] = 1; //La couleur du sommet de plus haut degré est défini sur rouge
            coloration2 = Coloration2(matrice, degré, coloration2, a); //On colorit si possible le reste du tableau
            Console.WriteLine(Resultat(coloration2,degré)); //Affichage du résultat de manière lisible
            Console.WriteLine("");

            Console.WriteLine("3-Coloration :");
            int[] coloration3 = new int[degré.GetLength(1)]; //On définit le tableau qui prendra le résultat de la coloration
            coloration3[0] = 1; //La couleur du sommet de plus haut degré est défini sur rouge
            coloration3 = Coloration3(matrice, degré, coloration3, a); //On colorit si possible le reste du tableau
            Console.WriteLine(Resultat(coloration3, degré)); //Affichage du résultat de manière lisible
            Console.WriteLine("");

            /*Console.WriteLine("4-Coloration :");
            int[] coloration4 = new int[degré.GetLength(1)]; //On définit le tableau qui prendra le résultat de la coloration
            coloration4[0] = 1; //La couleur du sommet de plus haut degré est défini sur rouge
            coloration4 = Coloration4(matrice, degré, coloration4, a); //On colorit si possible le reste du tableau
            Console.WriteLine(Resultat(coloration4, degré)); //Affichage du résultat de manière lisible
            Console.WriteLine("");*/

            Console.ReadKey();
        }

        /// <summary>
        /// Méthode qui renvoie le tableau colorié après tri dans le sens inverse et avec les couleurs correspondantes
        /// </summary>
        /// <param name="coloration"></param>
        /// <param name="degré"></param>
        /// <returns></returns>
        static string Resultat(int[] coloration,int[,] degré)
        {
            if (coloration == null)
            {
                return "Coloration Impossible";
            }
            else
            {
                string res = "";
                int[] temp = new int[coloration.Length];
                //On attribue aux sommets leurs couleurs correspondants en inversant le tri du début
                for (int i = 0; i < coloration.Length; i++)
                {
                    temp[degré[1, i]] = coloration[i];
                }
                for (int i = 0; i < coloration.Length; i++)
                {
                    res += "Le sommet numéro " + i + " est de couleur " + Couleur(temp[i]) + "\n";
                }
                return res;
            }
        }

        /// <summary>
        /// Attribue une couleur aux valeurs 1,2 et 3 pour une meilleure compréhension
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        static string Couleur(int n)
        {
            if (n == 1) { return "rouge"; }
            else
            {
                if (n == 2) { return "bleu"; }
                else
                {
                    if (n == 3) { return "vert"; }
                    else
                    {
                        if (n == 4) { return "jaune"; }
                        else
                        {
                            return "";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Méthode récursive qui colorie d'abord les sommets de plus hauts degrés en rouge s'ils ne sont pas voisins sinon en bleu s'ils ne sont pas voisins à nouveaux
        /// </summary>
        /// <param name="matrice"></param>
        /// <param name="degré"></param>
        /// <param name="coloration"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        static int[] Coloration2(int[,] matrice, int[,] degré, int[] coloration, int n)
        {
            if (n == degré.GetLength(1)) //La méthode récursive s'arrête quand tout le tableau degré a été parcourru
            {
                return coloration;
            }
            else
            {
                if (Voisin(matrice, degré, degré[1, n], coloration,1)) //si le sommet est voisin avec un sommet déjà colorié en rouge
                {
                    if (Voisin(matrice, degré, degré[1, n], coloration,2)) //si le sommet est voisin avec un sommet déjà colorié en bleu
                    {
                        coloration = null;
                        n = degré.GetLength(1);
                        return Coloration2(matrice, degré, coloration, n); //alors 2-coloration impossible donc on retourne un tableau null
                    }
                    else
                    {
                        coloration[n] = 2; //sinon on colorie ce sommet en bleu
                    }                    
                }
                else
                {
                    coloration[n] = 1; //sinon on colorie ce sommet en rouge
                }
                n++;
                return Coloration2(matrice, degré, coloration, n);
            }
        }

        /// <summary>
        /// Méthode récursive qui colorie d'abord les sommets de plus hauts degrés en rouge s'ils ne sont pas voisins sinon en bleu s'ils ne sont pas voisins à nouveaux
        /// </summary>
        /// <param name="matrice"></param>
        /// <param name="degré"></param>
        /// <param name="coloration"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        static int[] Coloration3(int[,] matrice, int[,] degré, int[] coloration, int n)
        {
            if (n == degré.GetLength(1)) //La méthode récursive s'arrête quand tout le tableau degré a été parcourru
            {
                return coloration;
            }
            else
            {
                if (Voisin(matrice, degré, degré[1, n], coloration,1)) //si le sommet est voisin avec un sommet déjà colorié en rouge
                {
                    if (Voisin(matrice, degré, degré[1, n], coloration,2)) //si le sommet est voisin avec un sommet déjà colorié en bleu
                    {
                        if (Voisin(matrice, degré, degré[1, n], coloration,3)) //si le sommet est voisin avec un sommet déjà colorié en vert
                        {
                            coloration = null;
                            n = degré.GetLength(1);
                            return Coloration3(matrice, degré, coloration, n); //alors 2-coloration impossible donc on retourne un tableau null
                        }
                        else
                        {
                            coloration[n] = 3; //sinon on colorie ce sommet en vert
                        }
                    }
                    else
                    {
                        coloration[n] = 2; //sinon on colorie ce sommet en bleu
                    }
                }
                else
                {
                    coloration[n] = 1; //sinon on colorie ce sommet en rouge
                }
                n++;
                return Coloration3(matrice, degré, coloration, n);
            }
        }

        static int[] Coloration4(int[,] matrice, int[,] degré, int[] coloration, int n)
        {
            if (n == degré.GetLength(1)) //La méthode récursive s'arrête quand tout le tableau degré a été parcourru
            {
                return coloration;
            }
            else
            {
                if (Voisin(matrice, degré, degré[1, n], coloration, 1)) //si le sommet est voisin avec un sommet déjà colorié en rouge
                {
                    if (Voisin(matrice, degré, degré[1, n], coloration, 2)) //si le sommet est voisin avec un sommet déjà colorié en bleu
                    {
                        if (Voisin(matrice, degré, degré[1, n], coloration, 3)) //si le sommet est voisin avec un sommet déjà colorié en vert
                        {
                            if (Voisin(matrice, degré, degré[1, n], coloration, 4)) //si le sommet est voisin avec un sommet déjà colorié en jaune
                            {
                                coloration = null;
                                n = degré.GetLength(1);
                                return Coloration4(matrice, degré, coloration, n); //alors 4-coloration impossible donc on retourne un tableau null
                            }
                            else
                            {
                                coloration[n] = 4; //sinon on colorie ce sommet en jaune
                            }
                        }
                        else
                        {
                            coloration[n] = 3; //sinon on colorie ce sommet en vert
                        }
                    }
                    else
                    {
                        coloration[n] = 2; //sinon on colorie ce sommet en bleu
                    }
                }
                else
                {
                    coloration[n] = 1; //sinon on colorie ce sommet en rouge
                }
                n++;
                return Coloration4(matrice, degré, coloration, n);
            }
        }

        /// <summary>
        /// Méthode qui détermine si le sommet est voisin d'un sommet de la couleur c
        /// </summary>
        /// <param name="matrice"></param>
        /// <param name="degré"></param>
        /// <param name="index"></param>
        /// <param name="coloration"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        static bool Voisin(int[,] matrice, int[,] degré, int index, int[] coloration, int c)
        {
            bool res = false;
            for (int i = 0; i < coloration.Length; i++)
            {
                if (coloration[i] == c && matrice[degré[1, i], index] == 1)
                {
                    res = true;
                }
            }
            return res;
        }

        /// <summary>
        /// Tri les colonnnes du tableau par ordre décroissant selon la première ligne
        /// </summary>
        /// <param name="tableau"></param>
        /// <returns></returns>
        static int[,] Tri(int[,] tableau)
        {
            bool res = false;
            int temp = 0;
            while (res == false)
            {
                res = true;
                for (int i = 0; i < tableau.GetLength(1) - 1; i++)
                {
                    if (tableau[0, i] < tableau[0, i + 1])
                    {
                        res = false;
                        temp = tableau[0, i];
                        tableau[0, i] = tableau[0, i + 1];
                        tableau[0, i + 1] = temp;
                        temp = tableau[1, i];
                        tableau[1, i] = tableau[1, i + 1];
                        tableau[1, i + 1] = temp;
                    }
                }
            }
            return tableau;
        }

        /// <summary>
        /// Rempli la matrice de liaisons de façon aléatoire et symétrique
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        static int[,] RemplissageAleatoire(int n)
        {
            int[,] matrice = new int[n, n];
            var rand = new Random();
            for (int ligne = 0; ligne < matrice.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matrice.GetLength(0); colonne++)
                {
                    if (ligne == colonne)
                    {
                        matrice[ligne, colonne] = 1;
                    }
                    if (colonne > ligne)
                    {
                        matrice[ligne, colonne] = rand.Next(2);
                        matrice[colonne, ligne] = matrice[ligne, colonne];
                    }
                }
            }
            return matrice;
        }
        
        /// <summary>
        /// Affiche la matrice
        /// </summary>
        /// <param name="matrice"></param>
        static void AfficherMatrice(int[,] matrice)
        {
            for (int ligne = 0; ligne < matrice.GetLength(0); ligne++)
            {
                for (int colonne = 0; colonne < matrice.GetLength(1); colonne++)
                {
                    Console.Write(" " + matrice[ligne, colonne]);
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }
    }
}

