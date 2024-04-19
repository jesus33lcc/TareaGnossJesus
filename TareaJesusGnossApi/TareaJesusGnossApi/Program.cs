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

        mResourceApi.ChangeOntology("tgenerojl.owl");
        string identificador = Guid.NewGuid().ToString();
        Genre generoPrueba = new(identificador);
        generoPrueba.Schema_name = "Género de Prueba";
        SecondaryResource generoSR = generoPrueba.ToGnossApiResource(mResourceApi, $"Genre_{identificador}");
        try
        {
            mResourceApi.LoadSecondaryResource(generoSR);
        }
        catch (Exception)
        {
            mResourceApi.Log.Error($"Exception -> Error en cargar el genero");
        }
    }
}