# QQQQQ mi version de VVVVV

ÍNDICE:
- [Historia](#historia)
- [Controles jugador](#control)
- [Mecánicas](#mecanicas)
- [Escenas / UI](#escenas)
- [Musica / Sonido](#audio)

--- 
<a name="historia"></a>
## HISTÓRIA DEL JUEGO:

El juego esta ambientado en una zona industrial con muchas fabricas de alta contaminación, y nosotros como jugador, representaremos a un pobre ninja que trabaja para una coorporación ultra secreta que quiere acabar con la contaminación mundial. Esta coorporación nos encargo una missión muy importante... Infiltrarnos a la sede central que controla toda esta zona indutrial. Debido a que somos novatos, acabamos atrapados dentro de la sede... Aqui empieza el juego, nuestro objetivo sera escapar de la sede independientemente de la missión que nos encargo la coorporación, ya que descubrimos cosas muuuy turbias. ¡Experimentos con seres! Y de ahi sale los enemigos de la cuales tenemos que huir. 

### Tras acabar el juego descubriras si nuestro ninja pudo huir o no de la sede...

--- 
<a name="control"></a>
## CONTROLES DEL JUGADOR:
### Movimientos:
- A: Para mover al jugador hacia la izquierda.
- B: Para mover al jugador hacia la derecha.
- Espacio: Para invertir la gravedad del jugador.
### Menus:
- Escapa (Esc): Para pausar el juego en partida.

--- 
<a name="mecanicas"></a>
## MECÁNICAS:
### Jugador:
- Puede desplazarse en los ejes horizontales.
- Puede invertir la gravedad para acceder a los bloques/superficies superiores.
### Enemigos:
- Tirador: Es estatico pero puede lanzar proyectiles ya sea en el eje horizontal o en el vertical. Si el jugador entra en contacto tanto con el propio enemigo como con el proyectil que lanza, muere.
- Caminante/Volador: Se desplazan des de un punto "A" a un punto "B" y al llegar al punto realizan un "descanso" antes de realizar la siguiente ruta, si el jugador entra dentro de su trayectoria y de su campo de vision, estos multiplican por 2 la velocidad de movimiento. La diferencia del volador y del caminante es que el primero puede moverse en verticalmente mientras que el otro tiene que estar constantemente en contacto con la superficio. Si el jugador entra en contacto con ellos, muere.
- Superficie peligrosa: Es la superficie con detalles de color "rojo" indicando que es zona de peligro, por lo tanto, si el jugador entra en contacto con ello, muere.
### Otros:
- Checkpoint: Para realizar en checkpoint, el jugador simplemente ha de pasar por las "maquinas rojas", una vez que la maquina inicie una animacion en su pantalla, significará que el checkpoint se realizo correctamente.

--- 
<a name="escenas"></a>
## ESCENAS / UI:
### Menu:
- Menu principal: En este menu podremos acceder al juego, a un breve tutorial, salir del juego i a otro menu de configuración.
- Configuración: En este menu podremos configurar el nivel del volumen de la música y salir del propio menu, el volumen es persistente.
- Pausa: Durante la partida, ya sea en el tutorial o en el juego, podremos pausar de dos maneras: haciendo click en el boton de pausa o pulsando la tecla "Esc". En este menu podemos realizar tres acciones, volver al punto de inicio de la partida (tutorial/juego), salir del menu de pausa y reanudar al juego, o volver directamente al menu principal.
- Fin de juego: Tras llegar a la fase final del juego, nos aparecera una pantalla de "GameOver" indicando el fin del juego, en esta pantalla, a parte de la musica de victoria, podremos realizar dos acciones: volver al inicio del juego de manera que volveriamos a jugar, o volver al menu principal.
- 
---
<a name="audio"></a>
## MUSICA / SONIDO:
### Musica:
- Menu: Hay una musica para cuando el usuario este en el menu.
- Partida: Hay otra para cuando esta en juego, es decir que tenga al personaje y pueda controlarlo.
### Sonido:
- Muerte: Cuando el jugador muere, a parte de verlo visualmente, escucharemos una sonido de muerte.
- Cambio gravedad: Cuando invertimos la gravedad del jugador, escucharemos un sonido.
- Fin de juego: Al llegar al final y "pasarse el juego", junto con el menu de "Game Over" escucharemos un sonido.
