﻿using Gnoss.ApiWrapper;
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

        string uri = "";
        string pOntology = "tpersonajl";
        string select = string.Empty, where = string.Empty;
        select += $@"SELECT DISTINCT ?s";
        where += $@" WHERE {{ ";
        where += $@"OPTIONAL{{?s ?p 'Persona de Prueba'.}}";
        where += $@"}}";
        SparqlObject resultadoQuery = mResourceApi.VirtuosoQuery(select, where, pOntology);
        if (resultadoQuery != null && resultadoQuery.results != null && resultadoQuery.results.bindings != null && resultadoQuery.results.bindings.Count > 0 && resultadoQuery.results.bindings.FirstOrDefault()?.Keys.Count > 0)
        {
            foreach (var item in resultadoQuery.results.bindings)
            {
                uri = item["s"].value;
            }
        }
        string[] partes = uri.Split('/', '_');
        string resourceId = partes[5];
        string articleID = partes[6];
        Person personaActor1Modificado = new Person();
        personaActor1Modificado.Schema_name = "Persona de Prueba Modificado";
        mResourceApi.ModifyComplexOntologyResource(personaActor1Modificado.ToGnossApiResource(mResourceApi, new List<string>() { "cine" }, new Guid(resourceId), new Guid(articleID)), false, true);

    }
}