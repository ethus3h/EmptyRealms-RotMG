CREATE DATABASE  IF NOT EXISTS `emptyrealms` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `emptyrealms`;

CREATE TABLE `accounts` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `uuid` varchar(128) NOT NULL,
  `password` varchar(256) NOT NULL,
  `name` varchar(64) NOT NULL DEFAULT 'DEFAULT',
  `rank` int(11) NOT NULL DEFAULT '0',
  `namechosen` tinyint(1) NOT NULL DEFAULT '0',
  `verified` tinyint(1) NOT NULL DEFAULT '1',
  `guild` int(11) NOT NULL,
  `guildRank` int(11) NOT NULL,
  `vaultCount` int(11) NOT NULL DEFAULT '1',
  `maxCharSlot` int(11) NOT NULL DEFAULT '1',
  `regTime` datetime NOT NULL DEFAULT '0000-00-00 00:00:00',
  `guest` tinyint(1) NOT NULL DEFAULT '0',
  `banned` tinyint(1) NOT NULL DEFAULT '0',
  `locked` varchar(512) NOT NULL,
  `ignored` varchar(512) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

CREATE TABLE `guildboards` (
  `guildId` int(11) NOT NULL,
  `text` varchar(1024) NOT NULL,
  PRIMARY KEY (`guildId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

CREATE TABLE `characters` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `accId` int(11) NOT NULL,
  `charId` int(11) NOT NULL,
  `charType` int(11) NOT NULL DEFAULT '782',
  `level` int(11) NOT NULL DEFAULT '1',
  `exp` int(11) NOT NULL DEFAULT '0',
  `fame` int(11) NOT NULL DEFAULT '0',
  `items` varchar(128) NOT NULL DEFAULT '-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1',
  `hp` int(11) NOT NULL DEFAULT '1',
  `mp` int(11) NOT NULL DEFAULT '1',
  `stats` varchar(64) NOT NULL DEFAULT '1, 1, 1, 1, 1, 1, 1, 1',
  `dead` tinyint(1) NOT NULL DEFAULT '0',
  `tex1` int(11) NOT NULL DEFAULT '0',
  `tex2` int(11) NOT NULL DEFAULT '0',
  `pet` int(11) NOT NULL DEFAULT '-1',
  `fameStats` varchar(128) NOT NULL,
  `createTime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `deathTime` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00',
  `totalFame` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=11 DEFAULT CHARSET=utf8;

CREATE TABLE `classstats` (
  `accId` int(11) NOT NULL,
  `objType` int(11) NOT NULL,
  `bestLv` int(11) NOT NULL DEFAULT '1',
  `bestFame` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`accId`,`objType`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

CREATE TABLE `death` (
  `accId` int(11) NOT NULL,
  `chrId` int(11) NOT NULL,
  `name` varchar(64) NOT NULL DEFAULT 'DEFAULT',
  `charType` int(11) NOT NULL DEFAULT '782',
  `tex1` int(11) NOT NULL DEFAULT '0',
  `tex2` int(11) NOT NULL DEFAULT '0',
  `items` varchar(128) NOT NULL DEFAULT '-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1',
  `fame` int(11) NOT NULL DEFAULT '0',
  `fameStats` varchar(128) NOT NULL,
  `totalFame` int(11) NOT NULL DEFAULT '0',
  `firstBorn` tinyint(1) NOT NULL,
  `killer` varchar(128) NOT NULL,
  `time` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`accId`,`chrId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

CREATE TABLE `guilds` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(128) NOT NULL DEFAULT 'DEFAULT_GUILD',
  `members` varchar(128) NOT NULL,
  `guildFame` int(11) NOT NULL,
  `totalGuildFame` int(11) NOT NULL,
  `level` int(11) NOT NULL DEFAULT '1',
  PRIMARY KEY (`id`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

CREATE TABLE `news` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `icon` varchar(16) NOT NULL DEFAULT 'info',
  `title` varchar(128) NOT NULL DEFAULT 'Default news title',
  `text` varchar(128) NOT NULL DEFAULT 'Default news text',
  `link` varchar(256) NOT NULL DEFAULT 'http://openrealms.net/',
  `date` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`)
) ENGINE=MyISAM AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

CREATE TABLE `stats` (
  `accId` int(11) NOT NULL,
  `fame` int(11) NOT NULL,
  `totalFame` int(11) NOT NULL,
  `credits` int(11) NOT NULL,
  `totalCredits` int(11) NOT NULL,
  PRIMARY KEY (`accId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

CREATE TABLE `vaults` (
  `accId` int(11) NOT NULL,
  `chestId` int(11) NOT NULL AUTO_INCREMENT,
  `items` varchar(128) NOT NULL,
  PRIMARY KEY (`accId`,`chestId`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

CREATE TABLE `clientnews` (
  `name` varchar(128) NOT NULL,
  `contents` varchar(256) NOT NULL,
  `date` timestamp DEFAULT CURRENT_TIMESTAMP,
  `newsType` int(11) NOT NULL,
  `cliVersion` varchar(128) NOT NULL,
  `link` varchar(256) NOT NULL,
  PRIMARY KEY (`date`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

CREATE TABLE `arenaleaderboard` (
  `wave` int(11) NOT NULL,
  `players` varchar(256) NOT NULL,
  PRIMARY KEY (`wave`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;