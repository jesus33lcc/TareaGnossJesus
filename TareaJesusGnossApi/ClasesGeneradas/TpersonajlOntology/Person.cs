using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Gnoss.ApiWrapper;
using Gnoss.ApiWrapper.Model;
using Gnoss.ApiWrapper.Helpers;
using GnossBase;
using Es.Riam.Gnoss.Web.MVC.Models;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;
using Gnoss.ApiWrapper.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace TpersonajlOntology
{
	[ExcludeFromCodeCoverage]
	public class Person : GnossOCBase
	{
		public Person() : base() { } 

		public Person(SemanticResourceModel pSemCmsModel, LanguageEnum idiomaUsuario) : base()
		{
			GNOSSID = pSemCmsModel.RootEntities[0].Entity.Uri;
			Try_birthPlace = new List<PlacePath>();
			SemanticPropertyModel propTry_birthPlace = pSemCmsModel.GetPropertyByPath("http://try.gnoss.com/ontology#birthPlace");
			if(propTry_birthPlace != null && propTry_birthPlace.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propTry_birthPlace.PropertyValues)
				{
					if(propValue.RelatedEntity!=null){
						PlacePath try_birthPlace = new PlacePath(propValue.RelatedEntity,idiomaUsuario);
						Try_birthPlace.Add(try_birthPlace);
					}
				}
			}
			this.Schema_name = GetPropertyValueSemCms(pSemCmsModel.GetPropertyByPath("http://schema.org/name"));
		}

		public Person(SemanticEntityModel pSemCmsModel, LanguageEnum idiomaUsuario) : base()
		{
			mGNOSSID = pSemCmsModel.Entity.Uri;
			mURL = pSemCmsModel.Properties.FirstOrDefault(p => p.PropertyValues.Any(prop => prop.DownloadUrl != null))?.FirstPropertyValue.DownloadUrl;
			Try_birthPlace = new List<PlacePath>();
			SemanticPropertyModel propTry_birthPlace = pSemCmsModel.GetPropertyByPath("http://try.gnoss.com/ontology#birthPlace");
			if(propTry_birthPlace != null && propTry_birthPlace.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propTry_birthPlace.PropertyValues)
				{
					if(propValue.RelatedEntity!=null){
						PlacePath try_birthPlace = new PlacePath(propValue.RelatedEntity,idiomaUsuario);
						Try_birthPlace.Add(try_birthPlace);
					}
				}
			}
			this.Schema_name = GetPropertyValueSemCms(pSemCmsModel.GetPropertyByPath("http://schema.org/name"));
		}

		public virtual string RdfType { get { return "http://schema.org/Person"; } }
		public virtual string RdfsLabel { get { return "http://schema.org/Person"; } }
		[LABEL(LanguageEnum.es,"Lugar de nacimiento")]
		[RDFProperty("http://try.gnoss.com/ontology#birthPlace")]
		public  List<PlacePath> Try_birthPlace { get; set;}

		[LABEL(LanguageEnum.es,"Nombre")]
		[RDFProperty("http://schema.org/name")]
		public  string Schema_name { get; set;}


		internal override void GetProperties()
		{
			base.GetProperties();
			propList.Add(new StringOntologyProperty("schema:name", this.Schema_name));
		}

		internal override void GetEntities()
		{
			base.GetEntities();
			if(Try_birthPlace!=null){
				foreach(PlacePath prop in Try_birthPlace){
					prop.GetProperties();
					prop.GetEntities();
					OntologyEntity entityPlacePath = new OntologyEntity("http://try.gnoss.com/ontology#PlacePath", "http://try.gnoss.com/ontology#PlacePath", "try:birthPlace", prop.propList, prop.entList);
				entList.Add(entityPlacePath);
				prop.Entity = entityPlacePath;
				}
			}
		} 
		public virtual ComplexOntologyResource ToGnossApiResource(ResourceApi resourceAPI)
		{
			return ToGnossApiResource(resourceAPI, new List<string>());
		}

		public virtual ComplexOntologyResource ToGnossApiResource(ResourceApi resourceAPI, List<string> listaDeCategorias)
		{
			return ToGnossApiResource(resourceAPI, listaDeCategorias, Guid.Empty, Guid.Empty);
		}

		public virtual ComplexOntologyResource ToGnossApiResource(ResourceApi resourceAPI, List<Guid> listaDeCategorias)
		{
			return ToGnossApiResource(resourceAPI, null, Guid.Empty, Guid.Empty, listaDeCategorias);
		}

		public virtual ComplexOntologyResource ToGnossApiResource(ResourceApi resourceAPI, List<string> listaDeCategorias, Guid idrecurso, Guid idarticulo, List<Guid> listaIdDeCategorias = null)
		{
			ComplexOntologyResource resource = new ComplexOntologyResource();
			Ontology ontology = null;
			GetEntities();
			GetProperties();
			if(idrecurso.Equals(Guid.Empty) && idarticulo.Equals(Guid.Empty))
			{
				ontology = new Ontology(resourceAPI.GraphsUrl, resourceAPI.OntologyUrl, RdfType, RdfsLabel, prefList, propList, entList);
			}
			else{
				ontology = new Ontology(resourceAPI.GraphsUrl, resourceAPI.OntologyUrl, RdfType, RdfsLabel, prefList, propList, entList,idrecurso,idarticulo);
			}
			resource.Id = GNOSSID;
			resource.Ontology = ontology;
			resource.TextCategories = listaDeCategorias;
			resource.CategoriesIds = listaIdDeCategorias;
			AddResourceTitle(resource);
			AddResourceDescription(resource);
			AddImages(resource);
			AddFiles(resource);
			return resource;
		}

		public override List<string> ToOntologyGnossTriples(ResourceApi resourceAPI)
		{
			List<string> list = new List<string>();
			AgregarTripleALista($"{resourceAPI.GraphsUrl}items/Person_{ResourceID}_{ArticleID}", "http://www.w3.org/1999/02/22-rdf-syntax-ns#type", $"<http://schema.org/Person>", list, " . ");
			AgregarTripleALista($"{resourceAPI.GraphsUrl}items/Person_{ResourceID}_{ArticleID}", "http://www.w3.org/2000/01/rdf-schema#label", $"\"http://schema.org/Person\"", list, " . ");
			AgregarTripleALista($"{resourceAPI.GraphsUrl}{ResourceID}", "http://gnoss/hasEntidad", $"<{resourceAPI.GraphsUrl}items/Person_{ResourceID}_{ArticleID}>", list, " . ");
			if(this.Try_birthPlace != null)
			{
			foreach(var item0 in this.Try_birthPlace)
			{
				AgregarTripleALista($"{resourceAPI.GraphsUrl}items/PlacePath_{ResourceID}_{item0.ArticleID}", "http://www.w3.org/1999/02/22-rdf-syntax-ns#type", $"<http://try.gnoss.com/ontology#PlacePath>", list, " . ");
				AgregarTripleALista($"{resourceAPI.GraphsUrl}items/PlacePath_{ResourceID}_{item0.ArticleID}", "http://www.w3.org/2000/01/rdf-schema#label", $"\"http://try.gnoss.com/ontology#PlacePath\"", list, " . ");
				AgregarTripleALista($"{resourceAPI.GraphsUrl}{ResourceID}", "http://gnoss/hasEntidad", $"<{resourceAPI.GraphsUrl}items/PlacePath_{ResourceID}_{item0.ArticleID}>", list, " . ");
				AgregarTripleALista($"{resourceAPI.GraphsUrl}items/Person_{ResourceID}_{ArticleID}", "http://try.gnoss.com/ontology#birthPlace", $"<{resourceAPI.GraphsUrl}items/PlacePath_{ResourceID}_{item0.ArticleID}>", list, " . ");
				if(item0.IdsTry_placeNode != null)
				{
					foreach(var item2 in item0.IdsTry_placeNode)
					{
						AgregarTripleALista($"{resourceAPI.GraphsUrl}items/PlacePath_{ResourceID}_{item0.ArticleID}", "http://try.gnoss.com/ontology#placeNode", $"<{item2}>", list, " . ");
					}
				}
			}
			}
				if(this.Schema_name != null)
				{
					AgregarTripleALista($"{resourceAPI.GraphsUrl}items/Person_{ResourceID}_{ArticleID}", "http://schema.org/name",  $"\"{GenerarTextoSinSaltoDeLinea(this.Schema_name)}\"", list, " . ");
				}
			return list;
		}

		public override List<string> ToSearchGraphTriples(ResourceApi resourceAPI)
		{
			List<string> list = new List<string>();
			List<string> listaSearch = new List<string>();
			AgregarTags(list);
			AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://www.w3.org/1999/02/22-rdf-syntax-ns#type", $"\"tpersonajl\"", list, " . ");
			AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://gnoss/type", $"\"http://schema.org/Person\"", list, " . ");
			AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://gnoss/hasfechapublicacion", $"{DateTime.Now.ToString("yyyyMMddHHmmss")}", list, " . ");
			AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://gnoss/hastipodoc", "\"5\"", list, " . ");
			AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://gnoss/hasfechamodificacion", $"{DateTime.Now.ToString("yyyyMMddHHmmss")}", list, " . ");
			AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://gnoss/hasnumeroVisitas", "0", list, " . ");
			AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://gnoss/hasprivacidadCom", "\"publico\"", list, " . ");
			AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://xmlns.com/foaf/0.1/firstName", $"\"{GenerarTextoSinSaltoDeLinea(this.Schema_name)}\"", list, " . ");
			AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://gnoss/hasnombrecompleto", $"\"{GenerarTextoSinSaltoDeLinea(this.Schema_name)}\"", list, " . ");
			string search = string.Empty;
			if(this.Try_birthPlace != null)
			{
			foreach(var item0 in this.Try_birthPlace)
			{
				AgregarTripleALista($"http://gnossAuxiliar/{ResourceID.ToString().ToUpper()}", "http://gnoss/hasEntidadAuxiliar", $"<{resourceAPI.GraphsUrl}items/PlacePath_{ResourceID}_{item0.ArticleID}>", list, " . ");
				AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://try.gnoss.com/ontology#birthPlace", $"<{resourceAPI.GraphsUrl}items/PlacePath_{ResourceID}_{item0.ArticleID}>", list, " . ");
				if(item0.IdsTry_placeNode != null)
				{
					foreach(var item2 in item0.IdsTry_placeNode)
					{
					Regex regex = new Regex(@"\/items\/.+_[0-9A-Fa-f]{8}[-]?(?:[0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}_[0-9A-Fa-f]{8}[-]?(?:[0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}");
					string itemRegex = item2;
					if (regex.IsMatch(itemRegex))
					{
						itemRegex = $"http://gnoss/{resourceAPI.GetShortGuid(itemRegex).ToString().ToUpper()}";
					}
					else
					{
						itemRegex = itemRegex.ToLower();
					}
						AgregarTripleALista($"{resourceAPI.GraphsUrl}items/PlacePath_{ResourceID}_{item0.ArticleID}", "http://try.gnoss.com/ontology#placeNode", $"<{itemRegex}>", list, " . ");
					}
				}
			}
			}
				if(this.Schema_name != null)
				{
					AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://schema.org/name",  $"\"{GenerarTextoSinSaltoDeLinea(this.Schema_name)}\"", list, " . ");
				}
			if (listaSearch != null && listaSearch.Count > 0)
			{
				foreach(string valorSearch in listaSearch)
				{
					search += $"{valorSearch} ";
				}
			}
			if(!string.IsNullOrEmpty(search))
			{
				AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://gnoss/search", $"\"{GenerarTextoSinSaltoDeLinea(search.ToLower())}\"", list, " . ");
			}
			return list;
		}

		public override KeyValuePair<Guid, string> ToAcidData(ResourceApi resourceAPI)
		{

			//Insert en la tabla Documento
			string tags = "";
			foreach(string tag in tagList)
			{
				tags += $"{tag}, ";
			}
			if (!string.IsNullOrEmpty(tags))
			{
				tags = tags.Substring(0, tags.LastIndexOf(','));
			}
			string titulo = $"{this.Schema_name.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace("\"", "\"\"").Replace("'", "#COMILLA#").Replace("|", "#PIPE#")}";
			string descripcion = $"{this.Schema_name.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace("\"", "\"\"").Replace("'", "#COMILLA#").Replace("|", "#PIPE#")}";
			string tablaDoc = $"'{titulo}', '{descripcion}', '{resourceAPI.GraphsUrl}', '{tags}'";
			KeyValuePair<Guid, string> valor = new KeyValuePair<Guid, string>(ResourceID, tablaDoc);

			return valor;
		}

		public override string GetURI(ResourceApi resourceAPI)
		{
			return $"{resourceAPI.GraphsUrl}items/TpersonajlOntology_{ResourceID}_{ArticleID}";
		}


		internal void AddResourceTitle(ComplexOntologyResource resource)
		{
			resource.Title = this.Schema_name;
		}

		internal void AddResourceDescription(ComplexOntologyResource resource)
		{
			resource.Description = this.Schema_name;
		}




	}
}
