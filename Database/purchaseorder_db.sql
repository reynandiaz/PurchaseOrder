-- phpMyAdmin SQL Dump
-- version 5.0.3
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jan 31, 2021 at 11:31 AM
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
('1', 'Item test1', NULL, '10.00', '2021-01-26 15:52:18', NULL, '2021-01-26 15:52:18', 'administrator'),
('2', 'testing item 2testing testing', NULL, '20.50', '2021-01-26 16:03:51', NULL, '2021-01-26 16:54:10', 'administrator'),
('3', 'testing item 3', NULL, '30.00', '2021-01-26 17:09:28', NULL, '2021-01-26 17:09:28', 'administrator'),
('4', 'testing item4', NULL, '50.00', '2021-01-27 15:13:08', NULL, '2021-01-28 21:43:22', 'administrator');

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
('1', 10, -11, 100, '2021-01-26 15:52:18', NULL, '2021-01-26 15:52:18', '0000-00-00 00:00:00'),
('2', 10, 25, 100, '2021-01-26 16:03:51', NULL, '2021-01-26 16:54:10', '0000-00-00 00:00:00'),
('3', 0, -20, 0, '2021-01-26 17:09:28', NULL, '2021-01-26 17:09:28', '0000-00-00 00:00:00'),
('4', 10, 94, 1000, '2021-01-27 15:13:08', NULL, '2021-01-28 21:43:22', '0000-00-00 00:00:00');

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
('2-20210126-0001', 1, '1', 13, '130.00', '2021-01-26 17:09:20', NULL, '2021-01-26 17:09:20', 'administrator'),
('2-20210126-0001', 2, '2', 8, '164.00', '2021-01-26 17:09:20', NULL, '2021-01-26 17:09:20', 'administrator'),
('2-20210126-0001', 3, '3', 3, '90.00', '2021-01-26 17:09:30', NULL, '2021-01-26 17:09:30', 'administrator'),
('2-20210127-0001', 1, '1', 11, '110.00', '2021-01-27 14:51:00', NULL, '2021-01-27 14:51:00', 'administrator'),
('2-20210127-0001', 2, '2', 3, '61.50', '2021-01-27 14:51:01', NULL, '2021-01-27 14:51:01', 'administrator'),
('2-20210127-0001', 3, '3', 3, '90.00', '2021-01-27 14:51:02', NULL, '2021-01-27 14:51:02', 'administrator'),
('2-20210127-0002', 1, '1', 9, '90.00', '2021-01-27 15:12:58', NULL, '2021-01-27 15:12:58', 'administrator'),
('2-20210127-0002', 2, '2', 4, '82.00', '2021-01-27 15:12:58', NULL, '2021-01-27 15:12:58', 'administrator'),
('2-20210127-0002', 3, '3', 6, '180.00', '2021-01-27 15:12:59', NULL, '2021-01-27 15:12:59', 'administrator'),
('2-20210127-0002', 4, '4', 2, '100.00', '2021-01-27 15:13:12', NULL, '2021-01-27 15:13:12', 'administrator'),
('2-20210127-0003', 1, '1', 9, '90.00', '2021-01-27 15:13:37', NULL, '2021-01-27 15:13:37', 'administrator'),
('2-20210127-0003', 2, '2', 3, '61.50', '2021-01-27 15:13:37', NULL, '2021-01-27 15:13:37', 'administrator'),
('2-20210127-0003', 3, '3', 3, '90.00', '2021-01-27 15:13:38', NULL, '2021-01-27 15:13:38', 'administrator'),
('2-20210127-0004', 1, '1', 14, '140.00', '2021-01-27 15:13:56', NULL, '2021-01-27 15:13:56', 'administrator'),
('2-20210127-0004', 2, '2', 5, '102.50', '2021-01-27 15:13:56', NULL, '2021-01-27 15:13:56', 'administrator'),
('2-20210127-0004', 3, '3', 4, '120.00', '2021-01-27 15:13:57', NULL, '2021-01-27 15:13:57', 'administrator'),
('2-20210127-0004', 4, '4', 2, '100.00', '2021-01-27 15:14:00', NULL, '2021-01-27 15:14:00', 'administrator'),
('2-20210127-0005', 1, '1', 1, '10.00', '2021-01-27 15:14:11', NULL, '2021-01-27 15:14:11', 'administrator'),
('2-20210127-0005', 2, '2', 1, '20.50', '2021-01-27 15:14:11', NULL, '2021-01-27 15:14:11', 'administrator'),
('2-20210128-0001', 1, '4', 6, '300.00', '2021-01-28 21:43:32', NULL, '2021-01-28 21:43:32', 'administrator'),
('2-20210129-0001', 1, '1', 4, '40.00', '2021-01-29 13:18:41', NULL, '2021-01-29 13:18:41', 'administrator'),
('2-20210129-0001', 2, '2', 1, '20.50', '2021-01-29 13:18:42', NULL, '2021-01-29 13:18:42', 'administrator'),
('2-20210129-0001', 3, '3', 1, '30.00', '2021-01-29 13:18:42', NULL, '2021-01-29 13:18:42', 'administrator');

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
('2-20210126-0001', 1, '1000.00', '384.00', 1, '', '2021-01-26 17:09:51', NULL, '2021-01-26 17:09:51', 'administrator'),
('2-20210127-0001', 1, '1000.00', '261.50', 1, '', '2021-01-27 14:51:16', NULL, '2021-01-27 14:51:16', 'administrator'),
('2-20210127-0002', 1, '1000.00', '452.00', 1, '', '2021-01-27 15:13:21', NULL, '2021-01-27 15:13:21', 'administrator'),
('2-20210127-0003', 1, '1000.00', '241.50', 2, '', '2021-01-27 15:13:50', NULL, '2021-01-27 15:13:50', 'administrator'),
('2-20210127-0004', 1, '10000.00', '462.50', 1, '', '2021-01-27 15:14:07', NULL, '2021-01-27 15:14:07', 'administrator'),
('2-20210127-0005', 1, '100.00', '30.50', 1, '', '2021-01-27 15:14:16', NULL, '2021-01-27 15:14:16', 'administrator'),
('2-20210128-0001', 1, '1000.00', '300.00', 1, '', '2021-01-28 21:43:38', NULL, '2021-01-28 21:43:38', 'administrator'),
('2-20210129-0001', 1, '1000.00', '90.50', 1, '', '2021-01-29 13:18:45', NULL, '2021-01-29 13:18:45', 'administrator');

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
('1', '1', '1', 2, '2021-01-29 20:04:53', NULL, '2021-01-29 20:04:53', 'administrator'),
('administrator', 'admin', 'admin', 1, '2021-01-09 12:00:23', NULL, '2021-01-09 12:00:23', 'administrator');

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
