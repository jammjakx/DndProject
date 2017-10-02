-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server versie:                10.1.26-MariaDB - mariadb.org binary distribution
-- Server OS:                    Win32
-- HeidiSQL Versie:              9.3.0.4984
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Databasestructuur van dnd_db wordt geschreven
DROP DATABASE IF EXISTS `dnd_db`;
CREATE DATABASE IF NOT EXISTS `dnd_db` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `dnd_db`;


-- Structuur van  tabel dnd_db.party wordt geschreven
DROP TABLE IF EXISTS `party`;
CREATE TABLE IF NOT EXISTS `party` (
  `Id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) DEFAULT NULL,
  `Description` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumpen data van tabel dnd_db.party: ~0 rows (ongeveer)
/*!40000 ALTER TABLE `party` DISABLE KEYS */;
/*!40000 ALTER TABLE `party` ENABLE KEYS */;


-- Structuur van  tabel dnd_db.party_users wordt geschreven
DROP TABLE IF EXISTS `party_users`;
CREATE TABLE IF NOT EXISTS `party_users` (
  `Id` int(11) NOT NULL,
  `Party_id` int(11) DEFAULT NULL,
  `User_id` int(11) DEFAULT NULL,
  `Start_date` date DEFAULT NULL,
  `End_date` date DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumpen data van tabel dnd_db.party_users: ~0 rows (ongeveer)
/*!40000 ALTER TABLE `party_users` DISABLE KEYS */;
/*!40000 ALTER TABLE `party_users` ENABLE KEYS */;


-- Structuur van  tabel dnd_db.roles wordt geschreven
DROP TABLE IF EXISTS `roles`;
CREATE TABLE IF NOT EXISTS `roles` (
  `Id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `Role_name` varchar(45) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Dumpen data van tabel dnd_db.roles: ~0 rows (ongeveer)
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;


-- Structuur van  tabel dnd_db.users wordt geschreven
DROP TABLE IF EXISTS `users`;
CREATE TABLE IF NOT EXISTS `users` (
  `Id` int(11) unsigned NOT NULL AUTO_INCREMENT,
  `Username` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `Email` varchar(255) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- Dumpen data van tabel dnd_db.users: ~0 rows (ongeveer)
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` (`Id`, `Username`, `Password`, `Email`) VALUES
	(1, 'peter', 'Studentje1', 'peter.snoek@gmail.com'),
	(2, 'mike', 'Studentje1', 'mike.deboer@hotmail.com');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
