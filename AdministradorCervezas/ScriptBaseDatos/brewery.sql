-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `address`
--

CREATE TABLE `address` (
  `add_id` int(11) NOT NULL,
  `add_street` varchar(50) NOT NULL,
  `add_suburb` varchar(50) NOT NULL,
  `add_number` varchar(15) NOT NULL,
  `add_zipCode` char(5) NOT NULL,
  `cit_code` char(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `address`
--

INSERT INTO `address` (`add_id`, `add_street`, `add_suburb`, `add_number`, `add_zipCode`, `cit_code`) VALUES
(1, '23B', 'Flowers', '23423A', '22555', 'TIJ'),
(2, '21', 'Infinity', '1090', '22333', 'MXC'),
(3, '11', 'Trivials', '10089T-5', '33445', 'MXC');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `beer`
--

CREATE TABLE `beer` (
  `be_id` int(11) NOT NULL,
  `be_grd_alcoh` double NOT NULL,
  `be_presentation` int(4) NOT NULL,
  `be_level_ferm` int(4) NOT NULL,
  `be_unitMeas` int(4) NOT NULL,
  `be_content` double NOT NULL,
  `br_code` char(3) NOT NULL,
  `cla_code` char(3) NOT NULL,
  `be_price` double NOT NULL,
  `be_image`mediumblob NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `beer`
--

INSERT INTO `beer` (`be_id`, `be_grd_alcoh`, `be_presentation`, `be_level_ferm`, `be_unitMeas`, `be_content`, `br_code`, `cla_code`, `be_price`, `be_image`) VALUES
(1, 3.5, 0, 1, 0, 600, 'TEC', 'LGH', 24.6, 'b1.jpg'),
(3, 5.2, 0, 1, 0, 600, 'COR', 'LGH', 39.99, 'b2.jpg'),
(4, 12.5, 0, 2, 0, 600, 'HEN', 'LGH', 35.2, 'b3.jpg'),
(5, 8.8, 2, 2, 1, 1, 'TEC', 'LGH', 50, 'b4.jpg'),
(6, 6.5, 2, 1, 1, 1.2, 'COR', 'LGH', 45.99, 'b5.jpg'),
(7, 3.5, 2, 1, 0, 500, 'TEC', 'LGH', 50, 'b6.jpg'),
(8, 6.5, 3, 2, 1, 4.5, 'TEC', 'TIT', 39.99, 'b7.jpg'),
(9, 8.8, 0, 1, 0, 700, 'HEN', 'LGH', 24.6, 'b8.jpg'),
(10, 5, 1, 0, 0, 600, 'TEC', 'RED', 35.2, 'b9.jpg'),
(11, 6.5, 0, 2, 0, 900, 'IND', 'AMB', 35.2, 'b10.jpg'),
(12, 14, 2, 2, 1, 2, 'Tec', 'Red', 45, 'b11.jpg'),
(13, 3, 2, 0, 0, 800, 'Ind', 'Amb', 45, 'b12.jpg'),
(14, 13, 0, 1, 0, 4, 'Cor', 'Lig', 58, 'b13.jpg');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `beer_type`
--

CREATE TABLE `beer_type` (
  `type_code` char(3) NOT NULL,
  `type_name` varchar(20) NOT NULL,
  `type_category` int(4) NOT NULL,
  `type_color` int(4) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `beer_type`
--

INSERT INTO `beer_type` (`type_code`, `type_name`, `type_category`, `type_color`) VALUES
('DRF', 'Draft', 0, 1),
('ICB', 'Ice Beer', 0, 0);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `brand`
--

CREATE TABLE `brand` (
  `br_code` char(3) NOT NULL,
  `br_name` varchar(50) NOT NULL,
  `cn_code` char(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `brand`
--

INSERT INTO `brand` (`br_code`, `br_name`, `cn_code`) VALUES
('BRA', 'Brabante', 'Col'),
('COR', 'Corona', 'MEX'),
('HEN', 'Heineken', 'PER'),
('IND', 'Indio', 'MEX'),
('MOD', 'Modelo', 'Mex'),
('SKL', 'Skol', 'Mex'),
('TEC', 'Tecate', 'MEX');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `city`
--

CREATE TABLE `city` (
  `cit_code` char(3) NOT NULL,
  `cit_name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `city`
--

INSERT INTO `city` (`cit_code`, `cit_name`) VALUES
('MXC', 'Mexicali'),
('TIJ', 'Tijuana');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `clasification`
--

CREATE TABLE `clasification` (
  `cla_code` char(3) NOT NULL,
  `cla_name` varchar(20) DEFAULT NULL,
  `type_code` char(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `clasification`
--

INSERT INTO `clasification` (`cla_code`, `cla_name`, `type_code`) VALUES
('AMB', 'Ambar', 'DRF'),
('LGH', 'Light', 'ICB'),
('RED', 'Red', 'DRF'),
('TIT', 'Titanium', 'DRF');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `clasification_brand`
--

CREATE TABLE `clasification_brand` (
  `cla_code` char(3) NOT NULL,
  `br_code` char(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `clasification_brand`
--

INSERT INTO `clasification_brand` (`cla_code`, `br_code`) VALUES
('AMB', 'IND'),
('LGH', 'COR'),
('LGH', 'TEC'),
('RED', 'TEC');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `country`
--

CREATE TABLE `country` (
  `cn_code` char(3) NOT NULL,
  `cn_name` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `country`
--

INSERT INTO `country` (`cn_code`, `cn_name`) VALUES
('COL', 'Colombia'),
('MEX', 'Mexico'),
('PER', 'Peru');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `customer`
--

CREATE TABLE `customer` (
  `cus_id` int(11) NOT NULL,
  `cus_firstName` varchar(30) NOT NULL,
  `cus_lastName` varchar(30) NOT NULL,
  `cus_email` varchar(55) NOT NULL,
  `cus_phone` char(10) NOT NULL,
  `add_id` int(11) DEFAULT NULL,
  `cus_stat` tinyint(4) NOT NULL,
  `cus_pass` varchar(25) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `customer`
--

INSERT INTO `customer` (`cus_id`, `cus_firstName`, `cus_lastName`, `cus_email`, `cus_phone`, `add_id`, `cus_stat`, `cus_pass`) VALUES
(2, 'Jesus Daniel', 'Melchor Bueno', 'dani123@gmail.com', '6649903982', 1, 1, 'Jorgss12'),
(3, 'Javier', 'Valencia Espinoza', 'Vale@gmail.com', '6647788767', 2, 1, 'Esperanza123'),
(4, 'Sergio Antonio', 'Gonzalez Cadena', 'gonzalezito@gmail.com', '6647788760', 3, 1, 'SergioEnamorado'),
(5, 'Jose Antonio', 'Islas Torres', 'Islas32@gmail.com', '6645545623', 1, 1, 'Nolose032'),
(6, 'Jose Antonio', 'Gonzalez Cadena', 'Islas232@gmail.com', '6649903932', 2, 1, 'QuizaDespues'),
(7, 'sergio', 'GC', 'admin@gmail.com', '6645998007', 3, 1, '123');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `orderdetail`
--

CREATE TABLE `orderdetail` (
  `ord_id` int(11) NOT NULL,
  `be_id` int(11) NOT NULL,
  `ordDet_quantity` int(6) NOT NULL,
  `ordDet_UnitPrice` double DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `orderdetail`
--

INSERT INTO `orderdetail` (`ord_id`, `be_id`, `ordDet_quantity`, `ordDet_UnitPrice`) VALUES
(1, 1, 11, 24.6),
(1, 3, 5, 39.99),
(2, 1, 26, 24.6),
(2, 3, 14, 39.99),
(2, 4, 9, 25.2),
(3, 1, 9, 24.6),
(3, 5, 5, 50),
(3, 6, 6, 45.99),
(4, 5, 18, 50),
(5, 5, 9, 50),
(6, 7, 5, 50);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `orders`
--

CREATE TABLE `orders` (
  `ord_id` int(11) NOT NULL,
  `ord_request_date` date NOT NULL,
  `ord_delivery_date` date NOT NULL,
  `cus_id` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `orders`
--

INSERT INTO `orders` (`ord_id`, `ord_request_date`, `ord_delivery_date`, `cus_id`) VALUES
(1, '2018-07-10', '2018-08-08', 2),
(2, '2018-07-26', '2018-09-20', 3),
(3, '2018-09-13', '2018-11-27', 2),
(4, '2018-09-06', '2018-10-04', 3),
(5, '2018-03-22', '2018-05-11', 4),
(6, '2018-09-19', '2018-10-20', 7);

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `address`
--
ALTER TABLE `address`
  ADD PRIMARY KEY (`add_id`),
  ADD KEY `cit_code` (`cit_code`);

--
-- Indices de la tabla `beer`
--
ALTER TABLE `beer`
  ADD PRIMARY KEY (`be_id`),
  ADD KEY `br_code` (`br_code`);

--
-- Indices de la tabla `beer_type`
--
ALTER TABLE `beer_type`
  ADD PRIMARY KEY (`type_code`);

--
-- Indices de la tabla `brand`
--
ALTER TABLE `brand`
  ADD PRIMARY KEY (`br_code`),
  ADD UNIQUE KEY `br_name` (`br_name`),
  ADD KEY `FK_Brand_Country` (`cn_code`);

--
-- Indices de la tabla `city`
--
ALTER TABLE `city`
  ADD PRIMARY KEY (`cit_code`);

--
-- Indices de la tabla `clasification`
--
ALTER TABLE `clasification`
  ADD PRIMARY KEY (`cla_code`),
  ADD KEY `type_code` (`type_code`);

--
-- Indices de la tabla `clasification_brand`
--
ALTER TABLE `clasification_brand`
  ADD PRIMARY KEY (`cla_code`,`br_code`),
  ADD KEY `br_code` (`br_code`),
  ADD KEY `cla_code` (`cla_code`);

--
-- Indices de la tabla `country`
--
ALTER TABLE `country`
  ADD PRIMARY KEY (`cn_code`);

--
-- Indices de la tabla `customer`
--
ALTER TABLE `customer`
  ADD PRIMARY KEY (`cus_id`),
  ADD KEY `add_id` (`add_id`);

--
-- Indices de la tabla `orderdetail`
--
ALTER TABLE `orderdetail`
  ADD PRIMARY KEY (`ord_id`,`be_id`),
  ADD KEY `be_id` (`be_id`),
  ADD KEY `ord_id` (`ord_id`);

--
-- Indices de la tabla `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`ord_id`),
  ADD KEY `FK_Order_Client` (`cus_id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `beer`
--
ALTER TABLE `beer`
  MODIFY `be_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT de la tabla `customer`
--
ALTER TABLE `customer`
  MODIFY `cus_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT de la tabla `orders`
--
ALTER TABLE `orders`
  MODIFY `ord_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `address`
--
ALTER TABLE `address`
  ADD CONSTRAINT `FK_Address_City` FOREIGN KEY (`cit_code`) REFERENCES `city` (`cit_code`);

--
-- Filtros para la tabla `beer`
--
ALTER TABLE `beer`
  ADD CONSTRAINT `FK_Beer_Brand` FOREIGN KEY (`br_code`) REFERENCES `brand` (`br_code`);

--
-- Filtros para la tabla `brand`
--
ALTER TABLE `brand`
  ADD CONSTRAINT `FK_Brand_Country` FOREIGN KEY (`cn_code`) REFERENCES `country` (`cn_code`);

--
-- Filtros para la tabla `clasification`
--
ALTER TABLE `clasification`
  ADD CONSTRAINT `FK_Clasification_Beer_Type` FOREIGN KEY (`type_code`) REFERENCES `beer_type` (`type_code`);

--
-- Filtros para la tabla `clasification_brand`
--
ALTER TABLE `clasification_brand`
  ADD CONSTRAINT `FK_Clasification_Brand_Brand` FOREIGN KEY (`br_code`) REFERENCES `brand` (`br_code`),
  ADD CONSTRAINT `FK_Clasification_Brand_Clasification` FOREIGN KEY (`cla_code`) REFERENCES `clasification` (`cla_code`);

--
-- Filtros para la tabla `customer`
--
ALTER TABLE `customer`
  ADD CONSTRAINT `FK_Customer_Address` FOREIGN KEY (`add_id`) REFERENCES `address` (`add_id`);

--
-- Filtros para la tabla `orderdetail`
--
ALTER TABLE `orderdetail`
  ADD CONSTRAINT `FK_OrderDetail_Beer` FOREIGN KEY (`be_id`) REFERENCES `beer` (`be_id`),
  ADD CONSTRAINT `FK_OrderDetail_Orders` FOREIGN KEY (`ord_id`) REFERENCES `orders` (`ord_id`);

--
-- Filtros para la tabla `orders`
--
ALTER TABLE `orders`
  ADD CONSTRAINT `FK_Order_Client` FOREIGN KEY (`cus_id`) REFERENCES `customer` (`cus_id`);
COMMIT;

