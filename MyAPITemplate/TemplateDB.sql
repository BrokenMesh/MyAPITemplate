CREATE DATABASE  IF NOT EXISTS `template_db` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `template_db`;
-- MySQL dump 10.13  Distrib 8.0.32, for Win64 (x86_64)
--
-- Host: localhost    Database: template_db
-- ------------------------------------------------------
-- Server version	8.0.32

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `tbl_todo`
--

DROP TABLE IF EXISTS `tbl_todo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_todo` (
  `Todo_id` binary(16) NOT NULL,
  `User_id` binary(16) NOT NULL,
  `Name` varchar(255) DEFAULT NULL,
  `Done` tinyint DEFAULT NULL,
  `CreatedAt` datetime DEFAULT NULL,
  `UpdatedAt` datetime DEFAULT NULL,
  PRIMARY KEY (`Todo_id`),
  KEY `todo_user_idx` (`User_id`),
  CONSTRAINT `todo_user` FOREIGN KEY (`User_id`) REFERENCES `tbl_user` (`User_id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_todo`
--

LOCK TABLES `tbl_todo` WRITE;
/*!40000 ALTER TABLE `tbl_todo` DISABLE KEYS */;
INSERT INTO `tbl_todo` VALUES (_binary '\nΩ\r;µ∫I;\œ\‹ÀÅ8ã',_binary '\»P\◊˜h\√Nî\‰\‹ÀÅ8:','Public-key human-resource orchestration',1,'2024-09-02 20:58:24','2024-09-02 20:58:24'),(_binary 'ZﬁºïîÉ\rO;\À\‹ÀÅ8ã',_binary '{=\ÏOÖBî\Â\‹ÀÅ8:','Persistent needs-based orchestration',0,'2024-09-02 20:58:24','2024-09-02 20:58:24'),(_binary 'o3\«TØ\ÌE;\—\‹ÀÅ8ã',_binary 'ßk•\‡ì™VBî\‚\‹ÀÅ8:','Innovative bi-directional toolset',1,'2024-09-02 20:58:24','2024-09-02 20:58:24'),(_binary 'r+è¿ìSjK;\Ã\‹ÀÅ8ã',_binary 'CÆ¯~Dî\‡\‹ÀÅ8:','User-centric directional monitoring',1,'2024-09-02 20:58:24','2024-09-02 20:58:24'),(_binary 'â\Z\‘Îû±A;\–\‹ÀÅ8ã',_binary '\⁄OT¿a;Bî\Ê\‹ÀÅ8:','Assimilated hybrid service-desk',0,'2024-09-02 20:58:24','2024-09-02 20:58:24'),(_binary 'ç\»ø\ \ÀA;\Œ\‹ÀÅ8ã',_binary 'ßk•\‡ì™VBî\‚\‹ÀÅ8:','Distributed non-volatile emulation',1,'2024-09-02 20:58:24','2024-09-02 20:58:24'),(_binary 'è\¬Sã\Î©vF;\Õ\‹ÀÅ8ã',_binary 'Ø\‘ˇ\Ë\n}]Lî\›\‹ÀÅ8:','Networked client-server alliance',1,'2024-09-02 20:58:24','2024-09-02 20:58:24'),(_binary 'ß\” ≥˙y˘H;\ \‹ÀÅ8ã',_binary 'π\√\Êj\ÃDMBî\Á\‹ÀÅ8:','Fully-configurable cohesive hub',1,'2024-09-02 20:58:24','2024-09-02 20:58:24'),(_binary '≥|¿0TE;\…\‹ÀÅ8ã',_binary '{=\ÏOÖBî\Â\‹ÀÅ8:','Realigned 4th generation alliance',0,'2024-09-02 20:58:24','2024-09-02 20:58:24'),(_binary '\¬\œmeôO;\»\‹ÀÅ8ã',_binary '\∆\ƒ\÷B/[tMî\„\‹ÀÅ8:','Balanced well-modulated Graphical User Interface',0,'2024-09-02 20:58:24','2024-09-02 20:58:24');
/*!40000 ALTER TABLE `tbl_todo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_user`
--

DROP TABLE IF EXISTS `tbl_user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tbl_user` (
  `User_id` binary(16) NOT NULL,
  `Username` varchar(255) NOT NULL,
  `Password` varchar(255) NOT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `CreatedAt` datetime NOT NULL,
  `UpdatedAt` datetime NOT NULL,
  PRIMARY KEY (`User_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_user`
--

LOCK TABLES `tbl_user` WRITE;
/*!40000 ALTER TABLE `tbl_user` DISABLE KEYS */;
INSERT INTO `tbl_user` VALUES (_binary 'CÆ¯~Dî\‡\‹ÀÅ8:','Ceylin','1234','Ceylin@gmail.com','2024-09-02 20:58:24','2024-09-02 20:58:24'),(_binary 'S6\ﬁ\»DAî\·\‹ÀÅ8:','Jordan','1234','Jordan@gmail.com','2024-09-02 20:58:24','2024-09-02 20:58:24'),(_binary '{=\ÏOÖBî\Â\‹ÀÅ8:','Andrew','1234','Andrew@gmail.com','2024-09-02 20:58:24','2024-09-02 20:58:24'),(_binary '¶≥è\0>\0úAî\ﬂ\‹ÀÅ8:','S√∂ren','1234','S√∂ren@gmail.com','2024-09-02 20:58:24','2024-09-02 20:58:24'),(_binary 'ßk•\‡ì™VBî\‚\‹ÀÅ8:','Arda','1234','Arda@gmail.com','2024-09-02 20:58:24','2024-09-02 20:58:24'),(_binary 'Ø\‘ˇ\Ë\n}]Lî\›\‹ÀÅ8:','Mandy','1234','Mandy@gmail.com','2024-09-02 20:58:22','2024-09-02 20:58:22'),(_binary 'π\√\Êj\ÃDMBî\Á\‹ÀÅ8:','BrokenMesh','Hallosaid1','elkordhicham@gmail.com','2024-09-02 20:58:24','2024-09-02 20:58:24'),(_binary '\∆\ƒ\÷B/[tMî\„\‹ÀÅ8:','Jette','1234','Jette@gmail.com','2024-09-02 20:58:24','2024-09-02 20:58:24'),(_binary '\»P\◊˜h\√Nî\‰\‹ÀÅ8:','Jimmy','1234','Jimmy@gmail.com','2024-09-02 20:58:24','2024-09-02 20:58:24'),(_binary '\⁄OT¿a;Bî\Ê\‹ÀÅ8:','Milan','1234','Milan@gmail.com','2024-09-02 20:58:24','2024-09-02 20:58:24'),(_binary '\‚™uñ/vBî\ﬁ\‹ÀÅ8:','Lee','1234','Lee@gmail.com','2024-09-02 20:58:24','2024-09-02 20:58:24');
/*!40000 ALTER TABLE `tbl_user` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-09-02 21:05:56
