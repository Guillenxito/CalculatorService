# CalculatorService

## Construir la aplicacion 
* Abrir los dos proyectos_'Client y CalculatorS'_ en visual estudio y ejecutarlos a la vez.

## Implementacion 
* Al ejecutar los proyecto saltara el terminal, el cual mostrará unos menús para utilizar la aplicación cliente, esta a su vez, establecerá conexión con el servidor enviándole una petición **POST** serializada en **JSON** con los parámetros necesarios para su dicho funcionamiento.
El servidor mediante su controlador _'CalculatorScontroller'_ leerá la petición y realizara las operaciones necesarias y devolverá un respuesta **JSON** al cliente.

## Requisitos del entorno
  * Visual studio 2015
