# CalculatorService

## Construir la aplicacion / Build the aplication.
* Abrir los dos proyectos_'Client y CalculatorS'_ en visual estudio y ejecutarlos a la vez.

* Open both proyects _'Client y CalculatorS'_ on visual studio and execute them at the same time.

## Implementacion / Implementation.
* Al ejecutar los proyecto saltara el terminal, el cual mostrará unos menús para utilizar la aplicación cliente, esta a su vez, establecerá conexión con el servidor enviándole una petición **POST** serializada en **JSON** con los parámetros necesarios para su dicho funcionamiento.
El servidor mediante su controlador _'CalculatorScontroller'_ leerá la petición y realizara las operaciones necesarias y devolverá un respuesta **JSON** al cliente.

* While you execute the project, the terminal will start and stancy, that will show you some menus in order to use the client aplicattion, and as well, it will stablish connection with the server sending a **POST** petition, serialized in **JSON** with the necessary parameters for its funtionability.

### Requisitos del entorno
  * Visual studio 2015
