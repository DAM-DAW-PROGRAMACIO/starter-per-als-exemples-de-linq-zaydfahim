using LinQ.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace LinQ
{
    public class ExemplesLinq
    {
        #region Nivell 1: Filtratge bàsic amb Where

        /// <summary>
        /// NIVELL 1.1 - Filtratge simple
        /// Concepte: Where filtra elements d'una col·lecció segons una condició.
        /// Exemple: Trobar elements d'un any específic.
        /// </summary>
        public static void Exemple01_FiltratgeSimple()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("=== EXEMPLE 1.1: Filtratge simple amb Where ===\n");

            var elementsDel75 = biblioteca.Elements.Where(x => x.Any == 1975);

            foreach(var element in elementsDel75)
            {
                Console.WriteLine(element);
            }
            
            
        }

        /// <summary>
        /// NIVELL 1.2 - Filtratge amb múltiples condicions
        /// Evolució: Combinar diverses condicions amb operadors lògics (&& i ||).
        /// Exemple: Elements d'un període i amb una etiqueta específica.
        /// </summary>
        public static void Exemple02_FiltratgeMultiple()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 1.2: Filtratge amb múltiples condicions ===\n");

            var elementsPeriodeEtiqueta = biblioteca.Elements.Where(x => x.Any >= 2007 && x.Any <= 2025 
                                                                    && x.Etiquetes.Contains("thriller", StringComparer.OrdinalIgnoreCase));

            foreach (var element in elementsPeriodeEtiqueta)
            {
                Console.WriteLine(element);
            }


        }

        /// <summary>
        /// NIVELL 1.3 - Filtratge per tipus (OfType)
        /// Concepte nou: OfType filtra i converteix elements a un tipus específic.
        /// Exemple: Obtenir només les Cançons de la biblioteca.
        /// </summary>
        public static void Exemple03_FiltratgePerTipus()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 1.3: Filtratge per tipus amb OfType ===\n");

            var elementsTipus = biblioteca.Elements.OfType<Canco>;

        }

        #endregion

        #region Nivell 2: Projecció amb Select

        /// <summary>
        /// NIVELL 2.1 - Projecció simple
        /// Concepte nou: Select transforma cada element en un nou valor.
        /// Exemple: Obtenir només els títols dels elements.
        /// </summary>
        public static void Exemple04_ProjecciSimple()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 2.1: Projecció simple amb Select ===\n");

            var titols = biblioteca.Elements.Select(x => x.Titol);

            foreach (var element in titols)
            {
                Console.WriteLine(element);
            }
        }

        /// <summary>
        /// NIVELL 2.2 - Projecció amb tipus anònim
        /// Evolució: Select pot crear objectes nous amb només les dades que necessitem.
        /// Exemple: Crear una llista amb títol, autor i any.
        /// </summary>
        public static void Exemple05_ProjecciAmbTipusAnonim()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 2.2: Projecció amb tipus anònim ===\n");   //Titol, Autor, Any, minuts

            var cancoProjeccions = biblioteca.Elements.OfType<Canco>().Select(
                x => new
                {
                    x.Titol,
                    x.Autor,
                    x.Any,
                    DuradaMinuts = x.DuradaSegons / 60.0
                }
                );
            foreach (var element in cancoProjeccions)
            {
                Console.WriteLine(element);
            }

        }

        /// <summary>
        /// NIVELL 2.3 - Combinació de Where i Select
        /// Evolució: Encadenar operacions per filtrar i projectar.
        /// Exemple: Títols de cançons d'un gènere específic.
        /// </summary>
        public static void Exemple06_WhereISelect()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 2.3: Combinació de Where i Select ===\n");

            var titolsCanco = biblioteca.Elements.OfType<Canco>().Where(x => x.Genere.Contains("Grunge")).Select(x => x.Titol);

            foreach (var element in titolsCanco)
            {
                Console.WriteLine(element);
            }

        }

        #endregion

        #region Nivell 3: Ordenació

        /// <summary>
        /// NIVELL 3.1 - Ordenació simple
        /// Concepte nou: OrderBy ordena elements de forma ascendent.
        /// Exemple: Ordenar cançons per títol alfabèticament.
        /// </summary>
        public static void Exemple07_OrdenacioSimple()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 3.1: Ordenació simple amb OrderBy ===\n");

            var cancoOrdenades = biblioteca.Elements.OfType<Canco>().OrderBy(x => x.Titol, StringComparer.OrdinalIgnoreCase);

            foreach (var element in cancoOrdenades)
            {
                Console.WriteLine(element);
            }

        }

        /// <summary>
        /// NIVELL 3.2 - Ordenació múltiple amb ThenBy
        /// Evolució: ThenBy permet ordenar per un segon criteri en cas d'empat.
        /// Exemple: Ordenar per any (descendent) i després per títol.
        /// </summary>
        public static void Exemple08_OrdenacioMultiple()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 3.2: Ordenació múltiple amb ThenBy ===\n");

            var ordenar = biblioteca.Elements.OfType<Canco>().OrderByDescending(x => x.Any).ThenBy(x => x.Titol);

            foreach (var element in ordenar)
            {
                Console.WriteLine(element);
            }



        }

        #endregion

        #region Nivell 4: Quantificadors

        /// <summary>
        /// NIVELL 4.1 - Existència amb Any
        /// Concepte nou: Any retorna true si almenys un element compleix la condició.
        /// Exemple: Hi ha alguna pel·lícula amb valoració >= 9?
        /// </summary>
        public static void Exemple09_QuantificadorAny()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 4.1: Quantificador Any ===\n");

            var peli = biblioteca.Elements.OfType<Pelicula>().Any(x => x.Valoracio >= 9);

            Console.WriteLine(peli);

            
        }

        /// <summary>
        /// NIVELL 4.2 - Universalitat amb All
        /// Concepte nou: All retorna true si TOTS els elements compleixen la condició.
        /// Exemple: Tots els llibres tenen més de 150 pàgines?
        /// </summary>
        public static void Exemple10_QuantificadorAll()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 4.2: Quantificador All ===\n");

            var llibre = biblioteca.Elements.OfType<Llibre>().All(x => x.Pagines > 150);
            Console.WriteLine(llibre);

            
        }

        #endregion

        #region Nivell 5: Cerca d'elements

        /// <summary>
        /// NIVELL 5.1 - Primer element amb First/FirstOrDefault
        /// Concepte nou: First obté el primer element (llança excepció si no n'hi ha).
        ///               FirstOrDefault retorna null/default si no n'hi ha cap.
        /// Exemple: Trobar la primera pel·lícula amb durada > 160 minuts.
        /// </summary>
        public static void Exemple11_CercaPrimerElement()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 5.1: Cerca del primer element ===\n");

            var duaradaPelicula = biblioteca.Elements.OfType<Pelicula>().FirstOrDefault(p => p.DuradaMinuts > 160);

            Console.WriteLine(duaradaPelicula);

            
        }

        /// <summary>
        /// NIVELL 5.2 - Element únic amb Single/SingleOrDefault
        /// Concepte nou: Single espera exactament 1 element (llança excepció si n'hi ha 0 o més d'1).
        /// Exemple: Trobar un element per títol (hauria de ser únic).
        /// </summary>
        public static void Exemple12_CercaElementUnic()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 5.2: Cerca d'element únic ===\n");

            var singleTitol = biblioteca.Elements.OfType<Pelicula>().SingleOrDefault(x => x.Titol == "Inception");

            Console.WriteLine(singleTitol);
        }

        #endregion

        #region Nivell 6: Agregació (Count, Sum, Average, Min, Max)

        /// <summary>
        /// NIVELL 6.1 - Comptar elements amb Count
        /// Concepte nou: Count retorna el nombre d'elements (amb o sense condició).
        /// Exemple: Comptar elements per tipus.
        /// </summary>
        public static void Exemple13_Agregacio_Count()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 6.1: Agregació - Count ===\n");

            var comptarElements = biblioteca.Elements.Count();
            var comptarElements2 = biblioteca.Elements.OfType<Canco>().Count();

            Console.WriteLine(comptarElements);
            Console.WriteLine(comptarElements2);
        }

        /// <summary>
        /// NIVELL 6.2 - Suma i mitjana amb Sum i Average
        /// Concepte nou: Sum suma valors numèrics, Average calcula la mitjana.
        /// Exemple: Durada total i mitjana de cançons.
        /// </summary>
        public static void Exemple14_Agregacio_SumAverage()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 6.2: Agregació - Sum i Average ===\n");

            var duradaTotalCanco = biblioteca.Elements.OfType<Canco>().Sum(x => x.DuradaSegons);
            TimeSpan totalSegons = TimeSpan.FromSeconds(duradaTotalCanco);
            Console.WriteLine(totalSegons);

            

            var duradaMitjanaCanco = biblioteca.Elements.OfType<Canco>().Average(x => x.DuradaSegons);
            TimeSpan mitjanaSegons = TimeSpan.FromSeconds(duradaMitjanaCanco);
            Console.WriteLine(mitjanaSegons);

            
        }

        /// <summary>
        /// NIVELL 6.3 - Mínim i màxim amb Min i Max
        /// Concepte nou: Min i Max troben el valor més petit i més gran.
        /// Exemple: Llibre amb menys i més pàgines.
        /// </summary>
        public static void Exemple15_Agregacio_MinMax()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 6.3: Agregació - Min i Max ===\n");

            var maxPag = biblioteca.Elements.OfType<Llibre>().Max(x => x.Pagines);
            var minPag = biblioteca.Elements.OfType<Llibre>().Min(x => x.Pagines);

            Console.WriteLine(maxPag);
            Console.WriteLine(minPag);

            //Llibre llibreMax = biblioteca.Elements.OfType<Llibre>().First(x => x.Pagines);


        }

        #endregion

        #region Nivell 7: Agrupació

        /// <summary>
        /// NIVELL 7.1 - Agrupació simple amb GroupBy
        /// Concepte nou: GroupBy agrupa elements segons una clau comuna.
        /// Exemple: Agrupar elements per any de creació.
        /// </summary>
        public static void Exemple16_AgrupacioSimple()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 7.1: Agrupació simple amb GroupBy ===\n");

            var agruparAnysCreacio = biblioteca.Elements.GroupBy(x => x.Any / 10 * 10).OrderBy(d => d.Key);
            
            foreach(var elem in agruparAnysCreacio)
            {
                Console.WriteLine($"ANY = {elem.Key}");
            }
        }

        /// <summary>
        /// NIVELL 7.2 - Agrupació amb projecció
        /// Evolució: Després d'agrupar, podem fer agregacions per cada grup.
        /// Exemple: Per cada gènere de cançó, mostrar nombre de cançons i durada total.
        /// </summary>
        public static void Exemple17_AgrupacioAmbAgregacio()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 7.2: Agrupació amb agregació ===\n");

            var agruparCanco = biblioteca.Elements.OfType<Canco>().GroupBy(x => x.Genere).
                Select(d => new
                {
                    Genere = d.Key,
                    NumCancons = d.Count(),
                    DuaradaTotal = TimeSpan.FromSeconds(d.Sum(c => c.DuradaSegons))
                }
            );
            foreach (var elem in agruparCanco)
            {
                Console.WriteLine($"GENERE = {elem.Genere}, NUMERO CANÇONS = {elem.NumCancons}, DURADA TOTAL = {elem.DuaradaTotal}");
            }


        }

        /// <summary>
        /// NIVELL 7.3 - Filtratge de grups (HAVING)
        /// Evolució: Podem filtrar grups després d'agrupar (equivalent a HAVING en SQL).
        /// Exemple: Només gèneres amb més de 2 cançons.
        /// </summary>
        public static void Exemple18_AgrupacioAmbFiltre()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 7.3: Agrupació amb filtre de grups ===\n");

            var agruparCanco = biblioteca.Elements.OfType<Canco>().GroupBy(c => c.Genere).Where(x => x.Count() > 2);

            foreach(var elem in agruparCanco)
            {
                Console.WriteLine($"Gènere: {elem.Key} (Total: {elem.Count()} cançons)");
            }

        }

        #endregion

        #region Nivell 8: Operacions de conjunt

        /// <summary>
        /// NIVELL 8.1 - Elements únics amb Distinct
        /// Concepte nou: Distinct elimina duplicats d'una seqüència.
        /// Exemple: Llista d'autors únics.
        /// </summary>
        public static void Exemple19_Distinct()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 8.1: Elements únics amb Distinct ===\n");

            var autorsUnics = biblioteca.Elements.Select(x => x.Autor).Distinct().OrderBy( e => e).ToList();

            foreach(var elem in autorsUnics)
            {
                Console.WriteLine(elem);
            }

            
        }

        /// <summary>
        /// NIVELL 8.2 - Unió de conjunts amb Union
        /// Concepte nou: Union combina dues seqüències eliminant duplicats.
        /// Exemple: Autors que han fet llibres O pel·lícules.
        /// </summary>
        public static void Exemple20_Union()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 8.2: Unió amb Union ===\n");

            var autorsLlibres = biblioteca.Elements.OfType<Llibre>().Select(x => x.Autor);

            var autorsPelicules = biblioteca.Elements.OfType<Pelicula>().Select(x => x.Autor);

            var unio = autorsLlibres.Union(autorsPelicules).OrderBy(a => a);

            foreach (var elem in unio)
            {
                Console.WriteLine(elem);
            }


        }

        /// <summary>
        /// NIVELL 8.3 - Intersecció amb Intersect
        /// Concepte nou: Intersect retorna elements presents en AMBDUES seqüències.
        /// Exemple: Autors que han creat tant llibres com pel·lícules.
        /// </summary>
        public static void Exemple21_Intersect()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 8.3: Intersecció amb Intersect ===\n");

            var autorsLlibres = biblioteca.Elements.OfType<Llibre>().Select(x => x.Autor);

            var autorsPelicules = biblioteca.Elements.OfType<Pelicula>().Select(x => x.Autor);

            var intersect = autorsLlibres.Intersect(autorsPelicules);

            foreach (var elem in intersect)
            {
                Console.WriteLine(elem);
            }
        }

        #endregion

        #region Nivell 9: Expansió de col·leccions (SelectMany)

        /// <summary>
        /// NIVELL 9.1 - Aplanar col·leccions amb SelectMany
        /// Concepte nou: SelectMany "aplana" col·leccions niuades en una sola llista.
        /// Exemple: Obtenir totes les etiquetes de tots els elements.
        /// </summary>
        public static void Exemple22_SelectMany()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 9.1: Aplanar col·leccions amb SelectMany ===\n");

            var etiquetes = biblioteca.Elements.SelectMany(e => e.Etiquetes).Distinct();

            foreach(var elem in etiquetes)
            {
                Console.WriteLine(elem);
            }
        }

        /// <summary>
        /// NIVELL 9.2 - SelectMany amb projecció
        /// Evolució: SelectMany pot projectar cada element de les col·leccions niuades.
        /// Exemple: Crear parelles (Element, Etiqueta).
        /// </summary>
        public static void Exemple23_SelectManyAmbProjecció()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 9.2: SelectMany amb projecció ===\n");

            var projeccio = biblioteca.Elements.SelectMany(e => e.Etiquetes,
                (elem ,e) => new
                {
                    Titol = elem.Titol,
                    Etiqueta = elem.Etiquetes,
                    Tipus = elem.GetType().Name
                }
                ).Where(e => e.Etiqueta.Contains("clàssic"));
            foreach(var elem in projeccio)
            {
                Console.WriteLine($"Trobat un element de tipus {elem.Tipus}: {elem.Titol}");
            }

            
        }

        #endregion

        #region Nivell 10: Consultes complexes

        /// <summary>
        /// NIVELL 10.1 - Consulta completa: Top autors
        /// Combina: GroupBy + Agregació + Ordenació + Projecció
        /// Exemple: Top 3 autors amb més obres, mostrant títols.
        /// </summary>
        public static void Exemple24_ConsultaComplexa_TopAutors()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 10.1: Consulta complexa - Top 3 autors ===\n");

            var topAutors = biblioteca.Elements.GroupBy(e => e.Autor).Select
                (g => new
                {
                    Autor = g.Key,
                    NumObres = g.Count(),
                    Titols = g.Select(e => e.Titol).OrderBy(t => t).ToList()
                }
                ).OrderByDescending(a => a.NumObres) 
                 .Take(3); ;
            foreach(var elem in topAutors)
            {
                var llistaTitols = string.Join(", ", elem.Titols);
                Console.WriteLine($"Autor {elem.Autor}, NumObres : {elem.NumObres}, Titol: {llistaTitols}");
            }

            
        }

        /// <summary>
        /// NIVELL 10.2 - Sintaxi de consulta (query syntax)
        /// Concepte: LINQ també ofereix una sintaxi similar a SQL.
        /// Exemple: La mateixa consulta amb sintaxi diferent.
        /// </summary>
        public static void Exemple25_SintaxiDeConsulta()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 10.2: Sintaxi de consulta (query syntax) ===\n");

            var elements1990 = from e in biblioteca.Elements
                               where e.Any >= 1990 & e.Any < 2000
                               group e by e.Autor into grup
                               select new 
                               { 
                                   Autor = grup.Key,
                                   Elements =grup.Count(),
                                   Titols = grup.Select(e => e.Titol).OrderBy(t => t).ToList()
                               };
            foreach (var item in elements1990)
            {
                Console.WriteLine($"Autor: {item.Autor} (Dècada 90)");
                Console.WriteLine($"Quantitat: {item.Elements}");
                Console.WriteLine($"Obres: {string.Join(", ", item.Titols)}");
                Console.WriteLine();
            }
        }

        /// <summary>
        /// NIVELL 10.3 - Consulta amb múltiples fonts (join implícit)
        /// Combina: SelectMany + Where amb relacions entre col·leccions
        /// Exemple: Cançons on el títol i l'àlbum comparteixen paraules.
        /// </summary>
        public static void Exemple26_ConsultaAmbJoin()
        {
            var biblioteca = GeneradorDades.CreaBiblioteca();
            Console.WriteLine("\n=== EXEMPLE 10.3: Consulta amb relacions (join implícit) ===\n");

            var autorsMultiples = from p in biblioteca.Elements.OfType<Pelicula>()
                                  from l in biblioteca.Elements.OfType<Llibre>()
                                  where p.Autor.Equals(l.Autor)
                                  select new
                                  {
                                      Nom = p.Autor,
                                      Pelicula = p.Titol,
                                      Llibre = l.Titol
                                  };


        }

        #endregion

        #region Mètode principal per executar tots els exemples

        /// <summary>
        /// Executa tots els exemples del tutorial en ordre
        /// </summary>
        public static void ExecutaComplet()
        {
            Console.WriteLine("*************************************************************************");


            // Nivell 1: Filtratge
            Exemple01_FiltratgeSimple();
            Exemple02_FiltratgeMultiple();
            Exemple03_FiltratgePerTipus();

            // Nivell 2: Projecció
            Exemple04_ProjecciSimple();
            Exemple05_ProjecciAmbTipusAnonim();
            Exemple06_WhereISelect();

            // Nivell 3: Ordenació
            Exemple07_OrdenacioSimple();
            Exemple08_OrdenacioMultiple();

            // Nivell 4: Quantificadors
            Exemple09_QuantificadorAny();
            Exemple10_QuantificadorAll();

            // Nivell 5: Cerca
            Exemple11_CercaPrimerElement();
            Exemple12_CercaElementUnic();

            // Nivell 6: Agregació
            Exemple13_Agregacio_Count();
            Exemple14_Agregacio_SumAverage();
            Exemple15_Agregacio_MinMax();

            // Nivell 7: Agrupació
            Exemple16_AgrupacioSimple();
            Exemple17_AgrupacioAmbAgregacio();
            Exemple18_AgrupacioAmbFiltre();

            // Nivell 8: Operacions de conjunt
            Exemple19_Distinct();
            Exemple20_Union();
            Exemple21_Intersect();

            // Nivell 9: SelectMany
            Exemple22_SelectMany();
            Exemple23_SelectManyAmbProjecció();

            // Nivell 10: Consultes complexes
            Exemple24_ConsultaComplexa_TopAutors();
            Exemple25_SintaxiDeConsulta();
            Exemple26_ConsultaAmbJoin();

            Console.WriteLine("*************************************************************************");

        }

        #endregion
    }
}
