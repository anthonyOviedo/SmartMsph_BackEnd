-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema smartmsph
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `smartmsph` ;

-- -----------------------------------------------------
-- Schema smartmsph
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `smartmsph` DEFAULT CHARACTER SET utf8 ;
USE `smartmsph` ;

-- -----------------------------------------------------
-- Table `smartmsph`.`person`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `smartmsph`.`person` ;

CREATE TABLE IF NOT EXISTS `smartmsph`.`person` (
  `Person_Id` BIGINT NOT NULL,
  `Name` VARCHAR(50) NOT NULL,
  `LastName1` VARCHAR(50) NOT NULL,
  `LastName2` VARCHAR(50) NOT NULL,
  `Email` VARCHAR(50) NULL DEFAULT NULL,
  `phoneNumber` VARCHAR(50) NULL DEFAULT NULL,
  PRIMARY KEY (`Person_Id`),
  UNIQUE INDEX `id_UNIQUE` (`Person_Id` ASC) VISIBLE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `smartmsph`.`department`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `smartmsph`.`department` ;

CREATE TABLE IF NOT EXISTS `smartmsph`.`department` (
  `Department_Id` SMALLINT NOT NULL,
  `DepartmentName` VARCHAR(45) NOT NULL,
  `Person_Id` BIGINT NULL DEFAULT NULL,
  PRIMARY KEY (`Department_Id`),
  UNIQUE INDEX `id_UNIQUE` (`Department_Id` ASC) VISIBLE,
  UNIQUE INDEX `DepartmentName_UNIQUE` (`DepartmentName` ASC) VISIBLE,
  INDEX `fk_Department_Person1_idx` (`Person_Id` ASC) VISIBLE,
  CONSTRAINT `fk_Department_Person1`
    FOREIGN KEY (`Person_Id`)
    REFERENCES `smartmsph`.`person` (`Person_Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `smartmsph`.`role`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `smartmsph`.`role` ;

CREATE TABLE IF NOT EXISTS `smartmsph`.`role` (
  `Role_id` SMALLINT NOT NULL AUTO_INCREMENT,
  `Name` VARCHAR(50) NOT NULL,
  PRIMARY KEY (`Role_id`))
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `smartmsph`.`user`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `smartmsph`.`user` ;

CREATE TABLE IF NOT EXISTS `smartmsph`.`user` (
  `User_Id` SMALLINT NOT NULL,
  `UserName` VARCHAR(45) NOT NULL,
  `Password` VARCHAR(45) NOT NULL,
  `Person_id` BIGINT NULL DEFAULT NULL,
  `Role_id` SMALLINT NOT NULL,
  `department_id` SMALLINT NULL DEFAULT NULL,
  PRIMARY KEY (`User_Id`),
  UNIQUE INDEX `id_UNIQUE` (`User_Id` ASC) VISIBLE,
  UNIQUE INDEX `UserName_UNIQUE` (`UserName` ASC) VISIBLE,
  INDEX `fk_citizen_user_citizen1_idx` (`Person_id` ASC) VISIBLE,
  INDEX `fk_User_Role1_idx` (`Role_id` ASC) VISIBLE,
  INDEX `fk_department_user_department_idx` (`department_id` ASC) VISIBLE,
  CONSTRAINT `fk_department_user_department`
    FOREIGN KEY (`department_id`)
    REFERENCES `smartmsph`.`department` (`Department_Id`),
  CONSTRAINT `fk_person_user_Person`
    FOREIGN KEY (`Person_id`)
    REFERENCES `smartmsph`.`person` (`Person_Id`),
  CONSTRAINT `fk_User_Role1`
    FOREIGN KEY (`Role_id`)
    REFERENCES `smartmsph`.`role` (`Role_id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `smartmsph`.`complain`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `smartmsph`.`complain` ;

CREATE TABLE IF NOT EXISTS `smartmsph`.`complain` (
  `Complain_id` SMALLINT NOT NULL AUTO_INCREMENT,
  `Description` VARCHAR(10000) NOT NULL,
  `state` VARCHAR(45) NOT NULL,
  `Person_Id` BIGINT NULL DEFAULT NULL,
  `User_id` SMALLINT NULL DEFAULT NULL,
  `employee` VARCHAR(200) NOT NULL,
  `employee_name` VARCHAR(200) NULL DEFAULT NULL,
  `Department_Id` SMALLINT NULL DEFAULT NULL,
  `fecha` DATETIME NOT NULL,
  `Answer` VARCHAR(10000) NULL DEFAULT NULL,
  `Departmet_idR` SMALLINT NULL DEFAULT NULL,
  PRIMARY KEY (`Complain_id`),
  INDEX `fk_complain_citizen1_idx` (`Person_Id` ASC) VISIBLE,
  INDEX `fk_complain_department1_idx` (`Department_Id` ASC) VISIBLE,
  INDEX `fk_User_complain1_idx` (`User_id` ASC) VISIBLE,
  INDEX `fk_Deparmetr_idx` (`Departmet_idR` ASC) VISIBLE,
  CONSTRAINT `fk_complain_department1`
    FOREIGN KEY (`Department_Id`)
    REFERENCES `smartmsph`.`department` (`Department_Id`),
  CONSTRAINT `fk_complain_Person`
    FOREIGN KEY (`Person_Id`)
    REFERENCES `smartmsph`.`person` (`Person_Id`),
  CONSTRAINT `fk_Deparmetr`
    FOREIGN KEY (`Departmet_idR`)
    REFERENCES `smartmsph`.`department` (`Department_Id`),
  CONSTRAINT `fk_User_complain1`
    FOREIGN KEY (`User_id`)
    REFERENCES `smartmsph`.`user` (`User_Id`))
ENGINE = InnoDB
AUTO_INCREMENT = 23
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `smartmsph`.`denounces`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `smartmsph`.`denounces` ;

CREATE TABLE IF NOT EXISTS `smartmsph`.`denounces` (
  `Denounces_id` SMALLINT NOT NULL AUTO_INCREMENT,
  `Description` VARCHAR(10000) NOT NULL,
  `State` VARCHAR(45) NOT NULL,
  `Person_id` BIGINT NULL DEFAULT NULL,
  `User_id` SMALLINT NULL DEFAULT NULL,
  `Department_id` SMALLINT NULL DEFAULT NULL,
  `Photo` MEDIUMTEXT NULL DEFAULT NULL,
  `Latitud` VARCHAR(100) NULL DEFAULT NULL,
  `Longitud` VARCHAR(100) NULL DEFAULT NULL,
  `Date` DATETIME NULL DEFAULT NULL,
  `Answer` VARCHAR(10000) NULL DEFAULT NULL,
  PRIMARY KEY (`Denounces_id`),
  INDEX `fk_denounces_citizen1_idx` (`Person_id` ASC) VISIBLE,
  INDEX `fk_User_denounces_idx` (`User_id` ASC) VISIBLE,
  INDEX `fk_Department_denounces_idx` (`Department_id` ASC) VISIBLE,
  CONSTRAINT `fk_denounces_department1`
    FOREIGN KEY (`Department_id`)
    REFERENCES `smartmsph`.`department` (`Department_Id`),
  CONSTRAINT `fk_denounces_person`
    FOREIGN KEY (`Person_id`)
    REFERENCES `smartmsph`.`person` (`Person_Id`),
  CONSTRAINT `fk_User_Denounces`
    FOREIGN KEY (`User_id`)
    REFERENCES `smartmsph`.`user` (`User_Id`))
ENGINE = InnoDB
AUTO_INCREMENT = 34
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `smartmsph`.`news`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `smartmsph`.`news` ;

CREATE TABLE IF NOT EXISTS `smartmsph`.`news` (
  `News_Id` SMALLINT NOT NULL AUTO_INCREMENT,
  `descri` VARCHAR(1000) NOT NULL,
  `photo` MEDIUMTEXT NOT NULL,
  `titulo` VARCHAR(200) NULL DEFAULT NULL,
  `Date` DATETIME NULL DEFAULT NULL,
  PRIMARY KEY (`News_Id`))
ENGINE = InnoDB
AUTO_INCREMENT = 26
DEFAULT CHARACTER SET = utf8mb4
COLLATE = utf8mb4_0900_ai_ci;


-- -----------------------------------------------------
-- Table `smartmsph`.`ticket`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `smartmsph`.`ticket` ;

CREATE TABLE IF NOT EXISTS `smartmsph`.`ticket` (
  `Ticket_id` SMALLINT NOT NULL AUTO_INCREMENT,
  `Department_id` SMALLINT NULL DEFAULT NULL,
  `Ticketcol` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`Ticket_id`),
  INDEX `fk_ticket_department1_idx` (`Department_id` ASC) VISIBLE,
  CONSTRAINT `fk_ticket_department1`
    FOREIGN KEY (`Department_id`)
    REFERENCES `smartmsph`.`department` (`Department_Id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;

USE `smartmsph` ;

-- -----------------------------------------------------
-- procedure ComplainInsert
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`ComplainInsert`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ComplainInsert`(PDescription varchar(10000), Pstate varchar(45), PPerson_Id bigint,PUser_id smallint,Pemployee varchar(45),Pemployee_name varchar(45),PDepartment_Id smallint,Pfecha VARCHAR(200) )
BEGIN
INSERT INTO complain 
(Description
, state
, Person_Id
, User_id
, employee
, employee_name
, Department_Id
,fecha
,Departmet_idR
,Answer) VALUES (
PDescription
, Pstate
, PPerson_Id
, PUser_id
, Pemployee
, Pemployee_name
, PDepartment_Id
,CURDATE()
,1
,'');
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure DeleteComplain
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`DeleteComplain`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteComplain`(in PComplain_id smallint)
BEGIN
DELETE FROM complain
WHERE Complain_id =PComplain_id;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure DeleteComplains
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`DeleteComplains`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteComplains`(in PComplain_id smallint)
BEGIN
DELETE FROM complain
WHERE Complain_id =PComplain_id;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure DeleteDenounce
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`DeleteDenounce`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteDenounce`(in PDenounce_id smallint)
BEGIN
DELETE FROM denounces
WHERE Denounces_id =PDenounce_id;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure DeleteDepartment
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`DeleteDepartment`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteDepartment`(in PDepartment_Id smallint)
BEGIN
DELETE FROM department
WHERE Department_Id =PDepartment_Id;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure DeleteNews
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`DeleteNews`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteNews`(IN PNews_Id smallint(20))
BEGIN
DELETE FROM news
WHERE News_Id =PNews_Id;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure DeleteUser
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`DeleteUser`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteUser`(in PUser_Id smallint)
BEGIN
DELETE FROM user
WHERE  User_Id=PUser_Id;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure Denounces_list
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`Denounces_list`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `Denounces_list`(in PUser_id smallint)
BEGIN
SELECT a.Denounces_id,a.Description,a.Department_id ,a.State,a.Photo,a.Latitud,a.Longitud,b.DepartmentName FROM denounces a 
JOIN department b ON a.Department_Id = b.Department_Id
where User_id=PUser_id 
ORDER BY a.Date desc
;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure FuncioXDeparta
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`FuncioXDeparta`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `FuncioXDeparta`( IN Pdepa SMALLINT)
BEGIN
SELECT p.Name , p.LastName1, p.LastName2, p.Person_Id
FROM smartmsph.user u
INNER JOIN  smartmsph.person p
where u.department_id=Pdepa and p.Person_Id=u.Person_id ;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure NewInsert
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`NewInsert`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `NewInsert`(Pnew_Id smallint ,Pdescripcion varchar(10000), Pphoto MEDIUMTEXT, Ptitulo varchar(10000))
BEGIN
	IF Pnew_Id = 0 then
    begin 
    insert into  news(descri,photo,titulo,Date) values(Pdescripcion,Pphoto,Ptitulo,CURDATE());

    end ;
    else  begin 
		UPDATE news SET descri = Pdescripcion, photo = 		Pphoto, titulo = Ptitulo , Date = CURDATE()
        where News_Id = Pnew_Id;
    end;
end if;
end$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure ObtainComplainXid
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`ObtainComplainXid`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ObtainComplainXid`(PUser_id SMALLINT)
BEGIN
 select a.*,b.DepartmentName  from  smartmsph.complain a
 join department b ON a.Department_Id =b.department_Id
 where User_id=PUser_id 
 ORDER BY a.Fecha desc;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure ObtainNews
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`ObtainNews`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ObtainNews`()
BEGIN
SELECT * FROM smartmsph.news 
order by Date;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure ObtainRole
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`ObtainRole`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ObtainRole`()
BEGIN
SELECT * FROM smartmsph.role;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure ObtainTicket
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`ObtainTicket`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `ObtainTicket`(IN PDepartment_id SMALLINT,PTicketcol varchar(45))
BEGIN
INSERT INTO ticket (Department_id,Ticketcol) VALUES (PDepartment_id,PTicketcol);
SELECT * FROM smartmsph.ticket WHERE Ticket_id = (SELECT MAX(Ticket_id) FROM smartmsph.ticket);
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SaveDenounce
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`SaveDenounce`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SaveDenounce`(pDenouce_id smallint ,PDescription varchar(10000), Pstate varchar(45),PPerson_id bigint,PUser_id smallint,PDepartment_id smallint,PPhoto MEDIUMTEXT,PLatitud varchar(100),PLongitud varchar(100))
BEGIN

if pDenouce_id = 0  then begin 
INSERT INTO denounces (
Description, 
State,
 Person_id, 
 User_id, 
 Department_id, 
 Photo,
 Latitud, 
 Longitud,
 Date,
 Answer) VALUES (PDescription
 ,Pstate
 ,PPerson_id
 ,PUser_id
 ,PDepartment_id
 ,PPhoto
 ,PLatitud
 ,PLongitud
 ,CURDATE()
 ,'');

end;
	else begin 
    update  denounces set Description=PDescription, Department_id=PDepartment_id, Photo=PPhoto, Latitud=PLatitud, Longitud=PLongitud,Date=CURDATE()  where Denounces_Id = pDenouce_id;
    
    end; 
 end if;



END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure SaveDepartment
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`SaveDepartment`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `SaveDepartment`(pDepartmet_Id smallint, pDepartmentName varchar(100),pPerson_Id INT)
BEGIN

if pDepartmet_Id = -1  then begin 



INSERT into department select  ifnull(max(Department_id),0) + 1
 ,pDepartmentName
 ,pPerson_Id
from department;

end;
	else begin 
    update  department set DepartmentName=pDepartmentName,
 Person_Id =  pPerson_Id  
    where Department_Id = pDepartmet_Id;
    
    end; 
 end if;



END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure TodoDepartamento
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`TodoDepartamento`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `TodoDepartamento`()
BEGIN
SELECT a.*,b.Name FROM smartmsph.department a JOIN person b ON a.Person_Id=b.Person_Id;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure UsersWithAll
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`UsersWithAll`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `UsersWithAll`()
BEGIN
SELECT a.*,b.Name,b.LastName1,b.LastName2,b.Email,b.phoneNumber,c.DepartmentName
FROM user a 
JOIN person b ON a.Person_Id=b.Person_Id
LEFT JOIN department c ON a.department_id = c.Department_Id
 where a.UserName=a.UserName and a.Person_Id =b.Person_Id;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure UsuarioRegistro
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`UsuarioRegistro`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `UsuarioRegistro`(Ppersona_id bigint, Pnombre varchar(100), Papellido1 varchar(100),
Papellido2 varchar(100),Pcorreo varchar(100), Ptelefono varchar(100), Pusuario_Id smallint, PnombreUsuario varchar(100),
Ppassword varchar(100), Prole smallint,  PDepartment smallint
)
BEGIN

insert into person values(Ppersona_id,Pnombre,Papellido1,Papellido2,Pcorreo,Ptelefono);

insert into user SELECT ifnull(MAX(User_Id),0) + 1
,PnombreUsuario
,Ppassword
,Ppersona_id
,Prole
,PDepartment  
FROM user;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure VerifyCredentials
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`VerifyCredentials`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `VerifyCredentials`( IN Puser varchar(45), IN Ppassword varchar(45))
BEGIN
SELECT a.*,b.Name,b.LastName1,b.LastName2,b.Email,b.phoneNumber,c.DepartmentName
FROM user a 
JOIN person b ON a.Person_Id=b.Person_Id
LEFT JOIN department c ON a.department_id = c.Department_Id
 where a.UserName=Puser and a.Password = Ppassword;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure allUsers
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`allUsers`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `allUsers`()
BEGIN
select * from  smartmsph.user;
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure funcionaryall
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`funcionaryall`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `funcionaryall`()
BEGIN
SELECT p.Name , p.LastName1, p.LastName2, p.Person_Id
FROM smartmsph.user u
, smartmsph.person p where u.Role_id=2 and   p.Person_Id=u.Person_Id; 
END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure updateComplain
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`updateComplain`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `updateComplain`( 
IN PComplain_id smallint(20),
IN PDescription varchar(200),
IN Pemployee varchar(45),
IN Pemployee_name varchar(45),
IN PDepartment_Id smallint,
IN Pfecha varchar(200)
)
BEGIN
UPDATE complain 
SET
Description = PDescription,
employee = Pemployee,
employee_name = Pemployee_name,
Department_Id=PDepartment_Id,
fecha= Pfecha

WHERE Complain_id = PComplain_id ;

END$$

DELIMITER ;

-- -----------------------------------------------------
-- procedure updateDepartment
-- -----------------------------------------------------

USE `smartmsph`;
DROP procedure IF EXISTS `smartmsph`.`updateDepartment`;

DELIMITER $$
USE `smartmsph`$$
CREATE DEFINER=`root`@`localhost` PROCEDURE `updateDepartment`( 
IN PDepartment_Id SMALLINT(20),
IN PDepartmentName VARCHAR(45),
IN PPerson_Id BIGINT(20) 
)
BEGIN

UPDATE department 
SET
DepartmentName = PDepartmentName,
Person_Id = PPerson_Id
WHERE Department_Id = PDepartment_Id ;

END$$

DELIMITER ;

SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
