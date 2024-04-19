using Gnoss.ApiWrapper;
using Gnoss.ApiWrapper.ApiModel;
using Gnoss.ApiWrapper.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Text.Json;
using TgenerojlOntology;
using TpeliculajlOntology;
using TpersonajlOntology;

internal class Program
{
    private static void Main(string[] args)
    {
        string pathOAuth = @"Config\oAuth_demo-gnoss-akademia-20-20.config";
        ResourceApi mResourceApi = new ResourceApi(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, pathOAuth));
        CommunityApi mCommunityApi = new CommunityApi(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, pathOAuth));
        ThesaurusApi mThesaurusApi = new ThesaurusApi(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, pathOAuth));
        UserApi mUserApi = new UserApi(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, pathOAuth));

        //Cargar un Género de Prueba[Commit: "Carga Género de Prueba"]

        //mResourceApi.ChangeOntology("tgenerojl.owl");
        //string identificador = Guid.NewGuid().ToString();
        //Genre generoPrueba = new(identificador);
        //generoPrueba.Schema_name = "Género de Prueba";
        //SecondaryResource generoSR = generoPrueba.ToGnossApiResource(mResourceApi, $"Genre_{identificador}");
        //try
        //{
        //    mResourceApi.LoadSecondaryResource(generoSR);
        //}
        //catch (Exception)
        //{
        //    mResourceApi.Log.Error($"Exception -> Error en cargar el genero");
        //}

        //Cargar una Persona de Prueba  [Commit: "Carga Persona de Prueba"]

        //mResourceApi.ChangeOntology("tpersonajl.owl");
        //Person personaPrueba = new Person();
        //personaPrueba.Schema_name = "Persona de Prueba";
        //ComplexOntologyResource resorceLoad = personaPrueba.ToGnossApiResource(mResourceApi, new List<string>() { "cine" }, Guid.NewGuid(), Guid.NewGuid());
        //mResourceApi.LoadComplexSemanticResource(resorceLoad);

        //Obtener los datos de la Persona de Prueba y Modificar el nombre de la Persona de Prueba [Commit:"Modificación persona de Prueba"]

        //string uri = "";
        //string pOntology = "tpersonajl";
        //string select = string.Empty, where = string.Empty;
        //select += $@"SELECT DISTINCT ?s";
        //where += $@" WHERE {{ ";
        //where += $@"OPTIONAL{{?s ?p 'Persona de Prueba'.}}";
        //where += $@"}}";
        //SparqlObject resultadoQuery = mResourceApi.VirtuosoQuery(select, where, pOntology);
        //if (resultadoQuery != null && resultadoQuery.results != null && resultadoQuery.results.bindings != null && resultadoQuery.results.bindings.Count > 0 && resultadoQuery.results.bindings.FirstOrDefault()?.Keys.Count > 0)
        //{
        //    foreach (var item in resultadoQuery.results.bindings)
        //    {
        //        uri = item["s"].value;
        //    }
        //}
        //string[] partes = uri.Split('/', '_');
        //string resourceId = partes[5];
        //string articleID = partes[6];
        //Person personaActor1Modificado = new Person();
        //personaActor1Modificado.Schema_name = "Persona de Prueba Modificado";
        //mResourceApi.ModifyComplexOntologyResource(personaActor1Modificado.ToGnossApiResource(mResourceApi, new List<string>() { "cine" }, new Guid(resourceId), new Guid(articleID)), false, true);

        //Eliminar a la Persona de Prueba[Commit: "Eliminación Persona de Prueba"]

        //En caso de que no se tenga la uri de la persona que queremos eliminar, hay que buscarla

        //string uri = "";
        //string pOntology = "tpersonajl";
        //string select = string.Empty, where = string.Empty;
        //select += $@"SELECT DISTINCT ?s";
        //where += $@" WHERE {{ ";
        //where += $@"OPTIONAL{{?s ?p 'Persona de Prueba Modificado'.}}";
        //where += $@"}}";
        //SparqlObject resultadoQuery = mResourceApi.VirtuosoQuery(select, where, pOntology);
        //if (resultadoQuery != null && resultadoQuery.results != null && resultadoQuery.results.bindings != null && resultadoQuery.results.bindings.Count > 0 && resultadoQuery.results.bindings.FirstOrDefault()?.Keys.Count > 0)
        //{
        //    foreach (var item in resultadoQuery.results.bindings)
        //    {
        //        uri = item["s"].value;
        //    }
        //}

        //string mensajeFalloBorradoRecPrincipal = $"Error en el borrado de la Persona {uri} -> Nombre:";
        //try
        //{
        //    mResourceApi.ChangeOntology("tpersonajl.owl");
        //    mResourceApi.PersistentDelete(mResourceApi.GetShortGuid(uri), true, true);
        //}
        //catch (Exception)
        //{
        //    mResourceApi.Log.Error($"Exception -> {mensajeFalloBorradoRecPrincipal}");
        //}

        //Cargar una Persona de Prueba  [Commit: "Carga Persona de Prueba 2"].

        //mResourceApi.ChangeOntology("tpersonajl.owl");
        //Person personaPrueba = new Person();
        //personaPrueba.Schema_name = "Persona de Prueba 2";
        //ComplexOntologyResource resorceLoad = personaPrueba.ToGnossApiResource(mResourceApi, new List<string>() { "cine" }, Guid.NewGuid(), Guid.NewGuid());
        //mResourceApi.LoadComplexSemanticResource(resorceLoad);

        //Cargar una Película de Prueba que tenga como Actor a la Persona de Prueba y como Género al Género de Prueba [Commit: "Carga Película de Prueba"].

        //string uriPersona = "";
        //string pOntology = "tpersonajl";
        //string select = string.Empty, where = string.Empty;
        //select += $@"SELECT DISTINCT ?s";
        //where += $@" WHERE {{ ";
        //where += $@"OPTIONAL{{?s ?p 'Persona de Prueba 2'.}}";
        //where += $@"}}";
        //SparqlObject resultadoQuery = mResourceApi.VirtuosoQuery(select, where, pOntology);
        //if (resultadoQuery != null && resultadoQuery.results != null && resultadoQuery.results.bindings != null && resultadoQuery.results.bindings.Count > 0 && resultadoQuery.results.bindings.FirstOrDefault()?.Keys.Count > 0)
        //{
        //    foreach (var item in resultadoQuery.results.bindings)
        //    {
        //        uriPersona = item["s"].value;
        //    }
        //}
        //string uriGenero = "";
        //pOntology = "tgenerojl";
        //string select2 = string.Empty, where2 = string.Empty;
        //select2 += $@"SELECT DISTINCT ?s";
        //where2 += $@" WHERE {{ ";
        //where2 += $@"OPTIONAL{{?s ?p 'Género de Prueba'.}}";
        //where2 += $@"}}";
        //SparqlObject resultadoQuery2 = mResourceApi.VirtuosoQuery(select, where, pOntology);
        //if (resultadoQuery2 != null && resultadoQuery2.results != null && resultadoQuery2.results.bindings != null && resultadoQuery2.results.bindings.Count > 0 && resultadoQuery2.results.bindings.FirstOrDefault()?.Keys.Count > 0)
        //{
        //    foreach (var item in resultadoQuery2.results.bindings)
        //    {
        //        uriGenero = item["s"].value;
        //    }
        //}
        //mResourceApi.ChangeOntology("tpeliculajl.owl");
        //Movie pelicula = new Movie();
        //pelicula.Schema_name = "Pelicula de Prueba";
        //pelicula.IdsSchema_actor = new List<string>() { uriPersona };
        //pelicula.IdsSchema_genre = new List<string>() { uriGenero };
        //ComplexOntologyResource resorceToLoad = pelicula.ToGnossApiResource(mResourceApi, new List<string>() { "cine" }, Guid.NewGuid(), Guid.NewGuid());
        //string idPeliculaCargada = mResourceApi.LoadComplexSemanticResource(resorceToLoad);

        //Planificar la carga de los datos contenidos en los .json de los ficheros contenidos en datospeliculas.zip (disponibles en el siguiente recurso).
        
        /*
         Lo que se debe hacer es recorrer los json que hay en la carpeta
        Y en cada iteracion hay que recoger los datos que nos interesan añadir en el recurso que vayamos a crear
        Pero antes de subir el recurso, hay que verificar los campos, porque no pueden existir en json o pueden estar vacios
        En ese caso o no se sube el recurso o se sube pero sin rellenar ese campo o con algun texto por defecto
         */


        //Programar la carga de los datos contenidos en los.json de los ficheros datospeliculas.zip[Commit: "Carga DataSet"]
        
        //Aqui introducir la ruta de la carpeta que contenga los json
        string pathDatosPeliculas = "C:\\Users\\Jesús\\Downloads\\Nueva carpeta (2)\\Nueva carpeta";
        string[] files = Directory.GetFiles(pathDatosPeliculas);
        foreach (string file in files)
        {
            try
            {
                string text = File.ReadAllText(file);
                JObject json = JObject.Parse(text);
                //Comprobar de que sea una pelicula y no una serie
                if (json.Property("Type") != null && json.Property("Type").Value.ToString().ToLower().Equals("movie"))
                {
                    Movie movie = new Movie();
                    //Solo recogere pocos datos, pero el proceso seria el mismo para la mayoria de propiedades
                    //Comprobar que el Json tenga la propiedad que estemos recogiendo y que ese valor no sea N/A
                    if (json.Property("Title").HasValues && !json.Property("Title").Value.ToString().Equals("N/A"))
                    {
                        movie.Schema_name = json.Property("Title").Value.ToString();
                    }
                    if (json.Property("Genre").HasValues && !json.Property("Genre").Value.ToString().Equals("N/A"))
                    {
                        movie.Schema_genre = new List<Genre>();
                        string[]generos = json.Property("Genre").Value.ToString().Split(',');
                        mResourceApi.ChangeOntology("tgenerojl.owl");
                        foreach (string generoN in generos)
                        {
                            Genre genero = new(Guid.NewGuid().ToString());
                            genero.Schema_name = generoN;
                            movie.Schema_genre.Add(genero);

                        }
                    }
                    if (json.Property("Actors").HasValues && !json.Property("Actors").Value.ToString().Equals("N/A"))
                    {
                        movie.Schema_actor = new List<Person>();
                        movie.IdsSchema_actor = new List<string>();
                        string[] nombreActores = json.Property("Actors").Value.ToString().Split(',');
                        foreach (string nombreActor in nombreActores)
                        {
                            //Comprobar en un diccionario si el actor existe, en ese caso añadirlo a la lista de actores de la pelicual
                            // si no, crear al actor, añadirlo al diccionario y a la lista de actores de la pelicula

                        }
                    }
                    mResourceApi.ChangeOntology("tpeliculajl.owl");
                    ComplexOntologyResource resorceToLoad = movie.ToGnossApiResource(mResourceApi, new List<string>() { "cine" }, Guid.NewGuid(), Guid.NewGuid());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Al archivo {file}, le falta algun campo");
            }

        }

    }
}