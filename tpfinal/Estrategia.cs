
using System;
using System.Collections.Generic;
using System.Drawing.Design;
using tp1;

namespace tpfinal
{

	public class Estrategia
	{
		private int CalcularDistancia(string str1, string str2)
		{
			// using the method
			// String[] strlist1 = str1.ToLower().Split(' ');
			// String[] strlist2 = str2.ToLower().Split(' ');
			// int distance = 1000;
			// foreach (String s1 in strlist1)
			// {
			// 	foreach (String s2 in strlist2)
			// 	{
			// 		distance = Math.Min(distance, Utils.calculateLevenshteinDistance(s1, s2));
			// 	}
			// }

			// return distance;

			str1 = (str1 ?? "").Replace(" ", "").ToLowerInvariant();
			str2 = (str2 ?? "").Replace(" ", "").ToLowerInvariant();

			return Utils.calculateLevenshteinDistance(str1, str2);
		}

		
		/**
 		* Consulta todas las Hojas.
		* @param arbol Árbol general de tipo DatoDistancia que se desea consultar
		* @return String concatenado con los datos de todas las hojas del árbol. 
		* Retorna cadena vacía si el árbol es null.
		*/	
		public String Consulta1(ArbolGeneral<DatoDistancia> arbol) {
    		if (arbol == null) {
        		return "";
    		}
    	return recorrerHojas(arbol);
		}        

		private String recorrerHojas(ArbolGeneral<DatoDistancia> arbol) {
			if (arbol.esHoja()) { 
				return arbol.getDatoRaiz() + "\n";
			} else {
					String resultado = "";
				foreach (var hijo in arbol.getHijos()){
						resultado += recorrerHojas(hijo);
					}
				return resultado;
			}
		}


		/**
 		* Consulta todos los Caminos hasta cada Hojas.
		* @param arbol ÁrbolGeneral de tipo DatoDistancia que se desea consultar
		* @return String que contiene todos los caminos hasta cada hoja.
		* Retorna cadena vacía si el árbol es null.
		*/	
		public string Consulta2(ArbolGeneral<DatoDistancia> arbol)
		{
			if (arbol == null)
				return string.Empty;

			string resultado = "";
			List<string> caminoActual = new List<string>();

			ObtenerCaminos(arbol, caminoActual, ref resultado);

			return resultado;
		}

		private void ObtenerCaminos(ArbolGeneral<DatoDistancia> nodo, List<string> camino, ref string resultado)
		{
			if (nodo == null)
				return;

			// Agrego el texto del nodo actual al camino
			camino.Add(nodo.getDatoRaiz().texto);

			// Si el nodo es hoja, agrego el camino al resultado
			if (nodo.getHijos() == null || nodo.getHijos().Count == 0)
			{
				resultado += string.Join(" -> ", camino) + "\n";
			}
			else
			{
				// Recorro cada hijo recursivamente
				foreach (var hijo in nodo.getHijos())
				{
					ObtenerCaminos(hijo, new List<string>(camino), ref resultado);
				}
			}
		}

		/**
 		* Consulta por Nivel.
		* @param arbol ÁrbolGeneral de tipo DatoDistancia que se desea consultar
		* @return String que contiene los datos almacenados en los nodos del árbol diferenciados 
		* por el nivel en que se encuentran.
		*/	
		public string Consulta3(ArbolGeneral<DatoDistancia> arbol)
		{
			if (arbol == null || arbol.getDatoRaiz() == null)
				return "";

			List<string> niveles = new List<string>();
			this.RecorrerPorNivel(arbol, 0, niveles);

			string resultado = "";
			for (int i = 0; i < niveles.Count; i++)
			{
				resultado += "Nivel " + i + ": " + niveles[i].Trim() + "\n";
			}

			return resultado.Trim();
		}

		private void RecorrerPorNivel(ArbolGeneral<DatoDistancia> nodo, int nivel, List<string> niveles)
		{
			if (nodo == null)
				return;

			// Si la lista no tiene espacio para este nivel, lo agregamos
			if (niveles.Count <= nivel)
				niveles.Add("");

			// Agregamos el texto del nodo al nivel correspondiente
			niveles[nivel] += nodo.getDatoRaiz().texto + " ";

			// Recorremos recursivamente los hijos
			foreach (var hijo in nodo.getHijos())
			{
				RecorrerPorNivel(hijo, nivel + 1, niveles);
			}
		}


		/*
		* Agregar Dato al Arbol BK
		* @param ArbolGeneral<DatoDistancia> arbol
		* @param DatoDistancia dato
		* @return void
		**/
		public void AgregarDato(ArbolGeneral<DatoDistancia> arbol, DatoDistancia dato)
		{
			if (arbol == null | arbol.getDatoRaiz() == null)
				return;
			
			int distancia = this.CalcularDistancia(arbol.getDatoRaiz().texto, dato.texto);

			ArbolGeneral<DatoDistancia>? hijo = this.getHijoConDistancia(arbol.getHijos(), distancia);

			if (hijo != null)
			{
				this.AgregarDato(hijo, dato);
			}
			else
			{
				DatoDistancia nuevoArbol = new DatoDistancia(distancia, dato.texto, dato.descripcion);
				arbol.agregarHijo(new ArbolGeneral<DatoDistancia>(nuevoArbol));
			}
		}

		// public void AgregarDato(ArbolGeneral<DatoDistancia> arbol, DatoDistancia dato)
		// {
		// 	if (arbol == null)
        // 		return;

		// 	if (arbol.getDatoRaiz() == null)
		// 	{
		// 		arbol.setDatoRaiz(dato);
		// 		return;
		// 	}
			

		// 	ArbolGeneral<DatoDistancia> arbolActual = arbol;
		// 	while (true)
		// 	{
		// 		int distancia = this.CalcularDistancia(arbolActual.getDatoRaiz().texto, dato.texto);

		// 		ArbolGeneral<DatoDistancia>? hijo = this.getHijoConDistancia(arbolActual.getHijos(), distancia);

		// 		if (hijo != null)
		// 		{
		// 			arbolActual = hijo;
		// 		}
		// 		else
		// 		{
		// 			DatoDistancia datoDistancia = new DatoDistancia(distancia, dato.texto, dato.descripcion);
		// 			arbolActual.agregarHijo(new ArbolGeneral<DatoDistancia>(datoDistancia));
		// 			break;
		// 		}
		// 	}
		// }


		public void Buscar(ArbolGeneral<DatoDistancia> arbol, string elementoABuscar, int umbral, List<DatoDistancia> collected)
		{
			if (arbol == null) return;

			int distanciaActual = CalcularDistancia(arbol.getDatoRaiz().texto, elementoABuscar);

			if (distanciaActual <= umbral)
				collected.Add(arbol.getDatoRaiz());

			foreach (var hijo in arbol.getHijos())
			{
				int distanciaHijo = hijo.getDatoRaiz().distancia; // distancia al padre
				if (distanciaHijo >= distanciaActual - umbral && distanciaHijo <= distanciaActual + umbral)
				{
					this.Buscar(hijo, elementoABuscar, umbral, collected);
				}
			}
		}

		public ArbolGeneral<DatoDistancia>? getHijoConDistancia(List<ArbolGeneral<DatoDistancia>> hijos, int distancia)
		{
			for (int i = 0; i < hijos.Count; i++)
			{
				ArbolGeneral<DatoDistancia> hijo = hijos[i];
				if (hijo.getDatoRaiz().distancia == distancia)
				{
					return hijo;
				}
			}
			return null;
		}
    }
}