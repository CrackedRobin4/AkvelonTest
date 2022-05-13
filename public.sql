/*
 Navicat Premium Data Transfer

 Source Server         : PostgreSQL
 Source Server Type    : PostgreSQL
 Source Server Version : 140002
 Source Host           : localhost:5432
 Source Catalog        : akvelonTest
 Source Schema         : public

 Target Server Type    : PostgreSQL
 Target Server Version : 140002
 File Encoding         : 65001

 Date: 13/05/2022 15:17:55
*/


-- ----------------------------
-- Sequence structure for project_project_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."project_project_id_seq";
CREATE SEQUENCE "public"."project_project_id_seq" 
INCREMENT 1
MAXVALUE 2147483647
CACHE 1;

-- ----------------------------
-- Sequence structure for task_task_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."task_task_id_seq";
CREATE SEQUENCE "public"."task_task_id_seq" 
INCREMENT 1
MAXVALUE 2147483647
CACHE 1;

-- ----------------------------
-- Table structure for project
-- ----------------------------
DROP TABLE IF EXISTS "public"."project";
CREATE TABLE "public"."project" (
  "project_id" int4 NOT NULL GENERATED ALWAYS AS IDENTITY (
INCREMENT 1
MAXVALUE 2147483647
CACHE 1
),
  "name" varchar(255) COLLATE "pg_catalog"."default",
  "start_date" date,
  "end_date" date,
  "status_id" int4,
  "priority" int4
)
;

-- ----------------------------
-- Table structure for status
-- ----------------------------
DROP TABLE IF EXISTS "public"."status";
CREATE TABLE "public"."status" (
  "status_id" int4 NOT NULL,
  "name" varchar(255) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Table structure for task
-- ----------------------------
DROP TABLE IF EXISTS "public"."task";
CREATE TABLE "public"."task" (
  "task_id" int4 NOT NULL GENERATED ALWAYS AS IDENTITY (
INCREMENT 1
MAXVALUE 2147483647
CACHE 1
),
  "status_id" int4,
  "project_id" int4,
  "priority" int4,
  "description" text COLLATE "pg_catalog"."default",
  "name" varchar(255) COLLATE "pg_catalog"."default"
)
;

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
ALTER SEQUENCE "public"."project_project_id_seq"
OWNED BY "public"."project"."project_id";
SELECT setval('"public"."project_project_id_seq"', 8, true);

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
ALTER SEQUENCE "public"."task_task_id_seq"
OWNED BY "public"."task"."task_id";
SELECT setval('"public"."task_task_id_seq"', 5, true);

-- ----------------------------
-- Primary Key structure for table project
-- ----------------------------
ALTER TABLE "public"."project" ADD CONSTRAINT "project_pkey" PRIMARY KEY ("project_id");

-- ----------------------------
-- Primary Key structure for table status
-- ----------------------------
ALTER TABLE "public"."status" ADD CONSTRAINT "status_pkey" PRIMARY KEY ("status_id");

-- ----------------------------
-- Primary Key structure for table task
-- ----------------------------
ALTER TABLE "public"."task" ADD CONSTRAINT "task_pkey" PRIMARY KEY ("task_id");

-- ----------------------------
-- Foreign Keys structure for table project
-- ----------------------------
ALTER TABLE "public"."project" ADD CONSTRAINT "fk_project_status_1" FOREIGN KEY ("status_id") REFERENCES "public"."status" ("status_id") ON DELETE CASCADE ON UPDATE CASCADE;

-- ----------------------------
-- Foreign Keys structure for table task
-- ----------------------------
ALTER TABLE "public"."task" ADD CONSTRAINT "fk_task_project_1" FOREIGN KEY ("project_id") REFERENCES "public"."project" ("project_id") ON DELETE CASCADE ON UPDATE CASCADE;
ALTER TABLE "public"."task" ADD CONSTRAINT "fk_task_status_1" FOREIGN KEY ("status_id") REFERENCES "public"."status" ("status_id") ON DELETE CASCADE ON UPDATE CASCADE;
