-- phpMyAdmin SQL Dump
-- version 5.0.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Feb 06, 2021 at 02:54 PM
-- Server version: 10.4.14-MariaDB
-- PHP Version: 7.2.34

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `purchaseorder_db`
--

-- --------------------------------------------------------

--
-- Stand-in structure for view `generatepccode`
-- (See below for the actual view)
--
CREATE TABLE `generatepccode` (
`MaxPCCode` bigint(12)
);

-- --------------------------------------------------------

--
-- Table structure for table `items`
--

CREATE TABLE `items` (
  `ItemCode` varchar(20) NOT NULL,
  `ItemName` varchar(100) DEFAULT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `UnitPrice` decimal(10,2) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `DeletedDate` datetime DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `items`
--

INSERT INTO `items` (`ItemCode`, `ItemName`, `Description`, `UnitPrice`, `CreatedDate`, `DeletedDate`, `UpdatedDate`, `UpdatedBy`) VALUES
('10544566251001', 'Apple Juice', NULL, '100.00', '2021-02-06 16:54:20', NULL, '2021-02-06 16:54:20', 'administrator'),
('667548084499', 'Winter Candy Apple Body Cream 500ml', NULL, '75.00', '2021-02-06 16:45:38', NULL, '2021-02-06 16:49:46', 'administrator'),
('667548084500', 'Winter Candy Apple Body Cream 700ml', NULL, '100.00', '2021-02-06 16:50:43', NULL, '2021-02-06 16:50:43', 'administrator');

-- --------------------------------------------------------

--
-- Stand-in structure for view `itemstocks`
-- (See below for the actual view)
--
CREATE TABLE `itemstocks` (
`ItemCode` varchar(20)
,`ItemName` varchar(100)
,`Description` varchar(255)
,`UnitPrice` decimal(10,2)
,`MinStocks` int(10)
,`CurrentStocks` int(10)
,`MaxStocks` int(10)
);

-- --------------------------------------------------------

--
-- Table structure for table `paymentmethod`
--

CREATE TABLE `paymentmethod` (
  `PaymentMethodID` int(11) NOT NULL,
  `PaymentMethod` varchar(50) DEFAULT NULL,
  `CreatedDate` datetime NOT NULL,
  `DeletedDate` datetime DEFAULT NULL,
  `UpdatedDate` datetime NOT NULL,
  `UpdatedBy` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `paymentmethod`
--

INSERT INTO `paymentmethod` (`PaymentMethodID`, `PaymentMethod`, `CreatedDate`, `DeletedDate`, `UpdatedDate`, `UpdatedBy`) VALUES
(1, 'Cash', '2021-01-13 00:00:20', NULL, '2021-01-13 00:00:20', 'administrator'),
(2, 'Credit Card', '2021-01-13 00:00:29', NULL, '2021-01-13 00:00:29', 'administrator');

-- --------------------------------------------------------

--
-- Table structure for table `stocks`
--

CREATE TABLE `stocks` (
  `ItemCode` varchar(20) NOT NULL,
  `MinStocks` int(10) NOT NULL,
  `CurrentStocks` int(10) NOT NULL,
  `MaxStocks` int(10) NOT NULL,
  `CreatedDate` datetime NOT NULL,
  `DeletedDate` datetime DEFAULT NULL,
  `UpdatedDate` datetime NOT NULL,
  `UpdatedBy` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `stocks`
--

INSERT INTO `stocks` (`ItemCode`, `MinStocks`, `CurrentStocks`, `MaxStocks`, `CreatedDate`, `DeletedDate`, `UpdatedDate`, `UpdatedBy`) VALUES
('10544566251001', 10, 49, 100, '2021-02-06 16:54:20', NULL, '2021-02-06 16:54:20', '0000-00-00 00:00:00'),
('667548084499', 10, 47, 100, '2021-02-06 16:45:38', NULL, '2021-02-06 16:49:46', '0000-00-00 00:00:00'),
('667548084500', 50, 100, 300, '2021-02-06 16:50:43', NULL, '2021-02-06 16:50:43', '0000-00-00 00:00:00');

-- --------------------------------------------------------

--
-- Table structure for table `transactiondetails`
--

CREATE TABLE `transactiondetails` (
  `TransactionCode` varchar(20) NOT NULL,
  `Seq` int(11) NOT NULL,
  `ItemCode` varchar(20) DEFAULT NULL,
  `Quantity` int(11) DEFAULT NULL,
  `Price` decimal(10,2) DEFAULT NULL,
  `CreatedDate` datetime NOT NULL,
  `DeletedDate` datetime DEFAULT NULL,
  `UpdatedDate` datetime NOT NULL,
  `UpdatedBy` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `transactiondetails`
--

INSERT INTO `transactiondetails` (`TransactionCode`, `Seq`, `ItemCode`, `Quantity`, `Price`, `CreatedDate`, `DeletedDate`, `UpdatedDate`, `UpdatedBy`) VALUES
('2-20210206-0001', 1, '667548084499', 1, '75.00', '2021-02-06 16:51:15', NULL, '2021-02-06 16:51:15', 'administrator'),
('2-20210206-0002', 1, '10544566251001', 1, '100.00', '2021-02-06 17:18:34', NULL, '2021-02-06 17:18:34', 'administrator'),
('2-20210206-0002', 2, '667548084499', 2, '150.00', '2021-02-06 17:18:39', NULL, '2021-02-06 17:18:39', 'administrator');

-- --------------------------------------------------------

--
-- Table structure for table `transactionheader`
--

CREATE TABLE `transactionheader` (
  `TransactionCode` varchar(20) NOT NULL,
  `TransactionType` int(11) DEFAULT NULL,
  `ReceivedPayment` decimal(10,2) DEFAULT NULL,
  `TotalPrice` decimal(10,2) DEFAULT NULL,
  `PaymentMethodID` int(11) DEFAULT NULL,
  `Comments` varchar(255) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `DeletedDate` datetime DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL,
  `UpdatedBy` varchar(20) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `transactionheader`
--

INSERT INTO `transactionheader` (`TransactionCode`, `TransactionType`, `ReceivedPayment`, `TotalPrice`, `PaymentMethodID`, `Comments`, `CreatedDate`, `DeletedDate`, `UpdatedDate`, `UpdatedBy`) VALUES
('2-20210206-0001', 1, '100.00', '75.00', 1, '', '2021-02-06 16:51:50', NULL, '2021-02-06 16:51:50', 'administrator'),
('2-20210206-0002', 1, '500.00', '250.00', 1, '', '2021-02-06 18:31:37', NULL, '2021-02-06 18:31:37', 'administrator');

-- --------------------------------------------------------

--
-- Table structure for table `userpc`
--

CREATE TABLE `userpc` (
  `PCCode` int(11) NOT NULL,
  `HostName` varchar(50) DEFAULT NULL,
  `CreatedDate` datetime DEFAULT NULL,
  `DeletedDate` datetime DEFAULT NULL,
  `UpdatedDate` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `userpc`
--

INSERT INTO `userpc` (`PCCode`, `HostName`, `CreatedDate`, `DeletedDate`, `UpdatedDate`) VALUES
(1, '192.168.56.1', '2021-01-10 14:09:40', NULL, '2021-01-10 14:09:40'),
(2, 'DESKTOP-1E8JGRB', '2021-01-20 09:18:05', NULL, '2021-01-20 09:18:05');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `UserCode` varchar(20) NOT NULL,
  `UserName` varchar(20) DEFAULT NULL,
  `Password` varchar(20) DEFAULT NULL,
  `UserRights` int(11) DEFAULT NULL,
  `CreatedDate` datetime NOT NULL,
  `DeletedDate` datetime DEFAULT NULL,
  `UpdatedDate` datetime NOT NULL,
  `UpdatedBy` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`UserCode`, `UserName`, `Password`, `UserRights`, `CreatedDate`, `DeletedDate`, `UpdatedDate`, `UpdatedBy`) VALUES
('administrator', 'admin', 'admin', 1, '2021-01-09 12:00:23', NULL, '2021-01-09 12:00:23', 'administrator'),
('user1', 'user', 'user', 2, '2021-01-29 20:04:53', NULL, '2021-01-29 20:04:53', 'administrator');

-- --------------------------------------------------------

--
-- Structure for view `generatepccode`
--
DROP TABLE IF EXISTS `generatepccode`;

CREATE ALGORITHM=UNDEFINED DEFINER=`admin`@`%` SQL SECURITY DEFINER VIEW `generatepccode`  AS SELECT CASE WHEN (select count(`userpc`.`PCCode`) AS `PCCode` from `userpc`) = 0 THEN 1 WHEN (select count(`userpc`.`PCCode`) AS `PCCode` from `userpc`) > 0 THEN (select max(`userpc`.`PCCode`) + 1 from `userpc`) ELSE NULL END AS `MaxPCCode` ;

-- --------------------------------------------------------

--
-- Structure for view `itemstocks`
--
DROP TABLE IF EXISTS `itemstocks`;

CREATE ALGORITHM=UNDEFINED DEFINER=`admin`@`%` SQL SECURITY DEFINER VIEW `itemstocks`  AS SELECT `i`.`ItemCode` AS `ItemCode`, `i`.`ItemName` AS `ItemName`, `i`.`Description` AS `Description`, `i`.`UnitPrice` AS `UnitPrice`, `s`.`MinStocks` AS `MinStocks`, `s`.`CurrentStocks` AS `CurrentStocks`, `s`.`MaxStocks` AS `MaxStocks` FROM (`items` `i` join `stocks` `s` on(`s`.`ItemCode` = `i`.`ItemCode`)) ORDER BY `i`.`CreatedDate` DESC ;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `items`
--
ALTER TABLE `items`
  ADD PRIMARY KEY (`ItemCode`);

--
-- Indexes for table `paymentmethod`
--
ALTER TABLE `paymentmethod`
  ADD PRIMARY KEY (`PaymentMethodID`);

--
-- Indexes for table `stocks`
--
ALTER TABLE `stocks`
  ADD PRIMARY KEY (`ItemCode`);

--
-- Indexes for table `transactiondetails`
--
ALTER TABLE `transactiondetails`
  ADD PRIMARY KEY (`TransactionCode`,`Seq`);

--
-- Indexes for table `transactionheader`
--
ALTER TABLE `transactionheader`
  ADD PRIMARY KEY (`TransactionCode`);

--
-- Indexes for table `userpc`
--
ALTER TABLE `userpc`
  ADD PRIMARY KEY (`PCCode`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`UserCode`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
