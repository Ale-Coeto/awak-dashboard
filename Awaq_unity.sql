create database AWAQ_UNITY;

use AWAQ_UNITY;

show tables;

select * from Minijuegos;
select * from Partida;
select * from Usuario;
select * from Zona_Usuario;
select * from Zonas;

INSERT INTO Zona_Usuario (ID_Usuario, ID_Zona)
    VALUES (1, 1), (1, 2), (1, 3), (1, 4);
INSERT INTO Zona_Usuario (ID_Usuario, ID_Zona)
    VALUES (2, 1), (2, 2), (2, 3);

SHOW PROCEDURE STATUS;


SELECT z.ID_Zona, z.nombre, z.descripcion 
FROM Zona_Usuario zu
join zonas z on zu.ID_Zona = z.ID_Zona 
WHERE ID_Usuario = 2;


DELIMITER //

-- drop procedure getZonasBYUserId;
CREATE PROCEDURE getZonasByUserId(IN user_id INT)
begin
    SELECT z.ID_Zona, z.nombre, z.descripcion 
	FROM Zona_Usuario zu
	join zonas z on zu.ID_Zona = z.ID_Zona 
	WHERE ID_Usuario = user_id;
end;

DELIMITER ;

DELIMITER //

-- drop procedure getMinijuegosZona;
CREATE PROCEDURE getMinijuegosZona(IN zona_id INT)
begin
    SELECT m.jefe, m.nombre, m.ID_minijuego
	FROM Minijuegos m
	where ID_Zona = zona_id;
end;

DELIMITER ;

DELIMITER //

-- drop procedure getMinijuegoDatos;
CREATE PROCEDURE getMinijuegoDatos(IN minijuego_id INT, in user_id INT)
begin
    SELECT p.puntaje, p.tiempo
	FROM Partida p
	where ID_Minijuego = minijuego_id and ID_Usuario = user_id;
end;

DELIMITER ;

DELIMITER //

-- drop procedure setPartida;
CREATE PROCEDURE setPartida(IN minijuego_id INT, in user_id INT, in nuevo_puntaje INT, in nuevo_tiempo INT)
begin
	DECLARE puntos_actuales INT DEFAULT NULL;

	SELECT puntaje INTO puntos_actuales
    FROM Partida
    WHERE ID_Minijuego = minijuego_id AND ID_Usuario = user_id;
    
   	IF puntos_actuales IS NOT NULL THEN
        IF nuevo_puntaje > puntos_actuales THEN
            UPDATE Partida
            SET puntaje = nuevo_puntaje, tiempo = nuevo_tiempo
            WHERE ID_Minijuego = minijuego_id AND ID_Usuario = user_id;
        END IF;
    ELSE
        -- If the entry does not exist, insert a new one
        INSERT INTO Partida (ID_Minijuego, ID_Usuario, puntaje, tiempo)
        VALUES (minijuego_id, user_id, nuevo_puntaje, nuevo_tiempo);
    END IF;
   
   	SELECT * 
    FROM Partida
    WHERE ID_Minijuego = minijuego_id AND ID_Usuario = user_id;
end;

DELIMITER ;

-- Test
call getZonasByUserId(1);
call getMinijuegosZona(2); 
call getMinijuegosZonaUser(2, 1);
call getMinijuegoDatos(1, 1); 
call setPartida(1, 1, 500, 5);

-- Redefinicion de tablas


