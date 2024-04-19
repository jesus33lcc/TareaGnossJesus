using Gnoss.ApiWrapper;
using Gnoss.ApiWrapper.ApiModel;
using Gnoss.ApiWrapper.Model;
using System;
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

        string uriPersona = "";
        string pOntology = "tpersonajl";
        string select = string.Empty, where = string.Empty;
        select += $@"SELECT DISTINCT ?s";
        where += $@" WHERE {{ ";
        where += $@"OPTIONAL{{?s ?p 'Persona de Prueba 2'.}}";
        where += $@"}}";
        SparqlObject resultadoQuery = mResourceApi.VirtuosoQuery(select, where, pOntology);
        if (resultadoQuery != null && resultadoQuery.results != null && resultadoQuery.results.bindings != null && resultadoQuery.results.bindings.Count > 0 && resultadoQuery.results.bindings.FirstOrDefault()?.Keys.Count > 0)
        {
            foreach (var item in resultadoQuery.results.bindings)
            {
                uriPersona = item["s"].value;
            }
        }
        string uriGenero = "";
        pOntology = "tgenerojl";
        string select2 = string.Empty, where2 = string.Empty;
        select2 += $@"SELECT DISTINCT ?s";
        where2 += $@" WHERE {{ ";
        where2 += $@"OPTIONAL{{?s ?p 'Género de Prueba'.}}";
        where2 += $@"}}";
        SparqlObject resultadoQuery2 = mResourceApi.VirtuosoQuery(select, where, pOntology);
        if (resultadoQuery2 != null && resultadoQuery2.results != null && resultadoQuery2.results.bindings != null && resultadoQuery2.results.bindings.Count > 0 && resultadoQuery2.results.bindings.FirstOrDefault()?.Keys.Count > 0)
        {
            foreach (var item in resultadoQuery2.results.bindings)
            {
                uriGenero = item["s"].value;
            }
        }
        mResourceApi.ChangeOntology("tpeliculajl.owl");
        Movie pelicula = new Movie();
        pelicula.Schema_name = "Pelicula de Prueba";
        pelicula.IdsSchema_actor = new List<string>() { uriPersona };
        pelicula.IdsSchema_genre = new List<string>() { uriGenero };
        ComplexOntologyResource resorceToLoad = pelicula.ToGnossApiResource(mResourceApi, new List<string>() { "cine" }, Guid.NewGuid(), Guid.NewGuid());
        string idPeliculaCargada = mResourceApi.LoadComplexSemanticResource(resorceToLoad);

    }
}