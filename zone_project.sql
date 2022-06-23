-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Generation Time: Jun 23, 2022 at 06:27 PM
-- Server version: 5.7.31
-- PHP Version: 7.3.21

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `zone_project`
--

-- --------------------------------------------------------

--
-- Table structure for table `capteurs`
--

DROP TABLE IF EXISTS `capteurs`;
CREATE TABLE IF NOT EXISTS `capteurs` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(20) NOT NULL,
  `description` varchar(20) NOT NULL,
  `statut` varchar(20) NOT NULL,
  `statut2` varchar(20) NOT NULL,
  `idZone` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_zone` (`idZone`)
) ENGINE=MyISAM AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `capteurs`
--

INSERT INTO `capteurs` (`id`, `nom`, `description`, `statut`, `statut2`, `idZone`) VALUES
(1, 'capt1', '', 'deconnecte', 'eteindre', 1),
(2, 'capt2', '', 'connecte', 'allumer', 1),
(3, 'capt3', '', 'deconnecte', 'eteindre', 2),
(4, 'capt4', '', 'deconnecte', 'eteindre', 2),
(5, 'capt5', '', 'connecte', 'allumer', 4),
(6, 'capt6', '', 'deconnecte', 'eteindre', 3),
(9, 'capt9', '', 'deconnecte', 'eteindre', 1),
(10, 'capt10', 'fdhsgh', 'deconnecte', 'eteindre', 4);

-- --------------------------------------------------------

--
-- Table structure for table `zone`
--

DROP TABLE IF EXISTS `zone`;
CREATE TABLE IF NOT EXISTS `zone` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nom` varchar(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `zone`
--

INSERT INTO `zone` (`id`, `nom`) VALUES
(1, 'zone 1'),
(2, 'zone 2'),
(3, 'zone 3'),
(4, 'zone 4');
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
