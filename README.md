# HyperMonitoringSystem-BETA
Aplicacion para monitoreo de camaras de vigilancia. 
Si solo quieres usar la interfaz MainWindows que es para 
solo ver video y "escuchar" su audio puedes ejecutarlo, pero
el audio se detendra en de forma repentina por un error el cual
reparare proximamente, se cual es la solucion pero requiere algo de 
tiempo.

Si vas a usar el Servidor debes abrir Hercules (antes de ejecutar
la aplicacion) y escuchar el puerto 1921, por ahi deberas enviarle
el siguiente comando:

<strong>HIT~Am-S:1234567||Recepcion||005||Alerta de fuego~~</strong>

//Descripcion<br>
Panel serial o Mac: 1234567<br>
Area name: Recepcion<br>
Zone number: 005<br>
Mensaje: Alerta de fuego<br>

Pero antes de todo debes cambiar los valores de Data.Credentials por
los tuyos.

Probablemente te lanzara un error de autentificacion por que no haz
activado o desactivado la opcion en Google para permitir una aplicacion
envie correos.

Cuando ejecutes esto el panel enviara esa informacion a un correo
establecido, el cual es "angeldavidmarte19@gmail.com" o por el cual
tu estableciste.

Creo que no me falto nada por aclarar.
