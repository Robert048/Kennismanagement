<?php
/**
 * Created by PhpStorm.
 * User: maaike
 * Date: 25-4-2016
 * Time: 17:02
 */

require_once('globals.php');

$pdo= new PDO(DB_HOST, DB_USER, DB_PASS);

if($pdo->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION));
{
    die($pdo->errorCode());
}