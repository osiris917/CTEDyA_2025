# CTEDyA_2025
1. Datos del autor del trabajo final

Nombre: David Ayala
Curso / Materia: [Completar según corresponda]
Trabajo: Implementación de Árbol General y Búsqueda por Distancia de Levenshtein

2. Diagrama de clases UML

Basado en tu código, las clases principales son:

ArbolGeneral<T>

Atributos:

T dato

List<ArbolGeneral<T>> hijos

Métodos:

getDatoRaiz(), setDatoRaiz(T dato)

getHijos(), agregarHijo(ArbolGeneral<T> hijo), eliminarHijo(ArbolGeneral<T> hijo)

esHoja(), altura(), nivel(T dato)

DatoDistancia

Atributos:

int distancia

string texto

string descripcion

Métodos:

Constructor (int distancia, string texto, string descripcion)

ToString()

Estrategia

Métodos:

AgregarDato(ArbolGeneral<DatoDistancia> arbol, DatoDistancia dato)

Buscar(ArbolGeneral<DatoDistancia> arbol, string elementoABuscar, int umbral, List<DatoDistancia> collected)

Consulta1, Consulta2, Consulta3

Métodos auxiliares: CalcularDistancia, ObtenerCaminos, RecorrerPorNivel, getHijoConDistancia

Utils

Métodos:

calculateLevenshteinDistance(string source, string target)

RemoveSpecialCharacters(string str)

init_patron(), set_patron(string), get_patron()

Relaciones:

ArbolGeneral<T> contiene otros ArbolGeneral<T> (composición)

Estrategia opera sobre ArbolGeneral<DatoDistancia>

DatoDistancia es usado por ArbolGeneral y Estrategia

Utils es clase de utilidades estáticas

En la presentación puedes dibujar un UML con ArbolGeneral en el centro, DatoDistancia asociado, Estrategia como clase que usa ArbolGeneral, y Utils como clase estática auxiliar.

3. Detalles de implementación y problemas encontrados

Implementación:

Se implementó un árbol general genérico (ArbolGeneral<T>), donde cada nodo puede tener múltiples hijos.

Se creó la clase DatoDistancia para almacenar datos con su distancia, texto y descripción.

La clase Estrategia implementa:

Inserción de datos según la distancia de Levenshtein al nodo padre.

Consultas de recorrido: preorden (Consulta1), todos los caminos (Consulta2) y por niveles (Consulta3).

Búsqueda de nodos dentro de un umbral de distancia (Buscar).

Utils implementa cálculo de distancia de Levenshtein y limpieza de strings.

Problemas encontrados:

Inicialmente la función de cálculo de distancia consideraba espacios y mayúsculas, se solucionó normalizando strings.

La inserción de nodos con la misma distancia necesitaba recursión para no perder la jerarquía del árbol.

Manejo de null en nodos hoja para consultas y búsquedas, se solucionó con validaciones.

Condiciones de ejecución:

El árbol requiere nodos inicializados antes de usar métodos de Estrategia.

Utils necesita que el archivo dataset.csv exista en la ruta datasets/.

4. Imágenes de pantallas y descripción

Si tu proyecto tiene interfaz gráfica (parece que usabas ProgressBar en comentarios):

Pantalla de carga: muestra el progreso de indexación de datos desde el CSV.

Pantalla de consultas: permite realizar las tres consultas (por preorden, caminos o niveles).

Pantalla de búsqueda: permite buscar un texto con un umbral de distancia y mostrar resultados.

En la presentación: colocar capturas de cada pantalla y explicar qué hace cada botón o campo de texto.

5. Ideas o sugerencias de mejora

Implementar interfaz gráfica completa (Windows Forms o WPF) para mostrar árbol y resultados.

Optimizar Buscar usando estructura de BK-tree para mayor eficiencia en búsquedas por distancia.

Guardar el árbol en un archivo serializado para no volver a indexar cada vez.

Agregar edición y eliminación de nodos desde la interfaz.

Permitir búsquedas con patrones más complejos o tolerancia a errores en varios campos.

6. Conclusión o reflexión

Se aprendió a manejar estructuras de datos dinámicas como árboles generales.

Se comprendió el concepto de distancia de Levenshtein y su aplicación en búsquedas difusas.

Se mejoró la capacidad de depuración y manejo de errores en estructuras recursivas.

La experiencia refuerza la importancia de planificar la inserción y búsqueda en estructuras jerárquicas para eficiencia y claridad del código.
