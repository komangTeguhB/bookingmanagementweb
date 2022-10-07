--
-- PostgreSQL database dump
--

-- Dumped from database version 9.6.5
-- Dumped by pg_dump version 9.6.5

-- Started on 2017-10-03 11:17:34

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 2161 (class 1262 OID 16393)
-- Dependencies: 2160
-- Name: EAP_BMS; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON DATABASE "EAP_BMS" IS 'Booking Management System Database';


--
-- TOC entry 1 (class 3079 OID 12387)
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- TOC entry 2163 (class 0 OID 0)
-- Dependencies: 1
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET search_path = public, pg_catalog;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 187 (class 1259 OID 16407)
-- Name: Client; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "Client" (
    "Id" integer NOT NULL,
    "Name" character varying(100) NOT NULL,
    "Email" character varying(100) NOT NULL,
    "Phone" integer NOT NULL,
    "Address" character varying(200) NOT NULL,
    "Status" boolean NOT NULL
);


ALTER TABLE "Client" OWNER TO postgres;

--
-- TOC entry 185 (class 1259 OID 16394)
-- Name: ConsultationType; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "ConsultationType" (
    "Id" integer NOT NULL,
    "TypeName" text NOT NULL,
    "Duration" integer NOT NULL
);


ALTER TABLE "ConsultationType" OWNER TO postgres;

--
-- TOC entry 189 (class 1259 OID 16431)
-- Name: SessionsBooked; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "SessionsBooked" (
    "Id" integer NOT NULL,
    "Client" integer NOT NULL,
    "StaffSession" integer NOT NULL,
    "IsActive" boolean NOT NULL
);


ALTER TABLE "SessionsBooked" OWNER TO postgres;

--
-- TOC entry 186 (class 1259 OID 16402)
-- Name: Staff; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "Staff" (
    "Id" integer NOT NULL,
    "Name" character varying(100) NOT NULL,
    "Email" character varying(100) NOT NULL,
    "Phone" integer NOT NULL,
    "Role" character varying(50) NOT NULL,
    "Status" boolean NOT NULL,
    "Address" character varying(200) NOT NULL
);


ALTER TABLE "Staff" OWNER TO postgres;

--
-- TOC entry 188 (class 1259 OID 16412)
-- Name: StaffSession; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "StaffSession" (
    "Id" integer NOT NULL,
    "Date" date NOT NULL,
    "Time" time without time zone NOT NULL,
    "ConsultationType" integer NOT NULL,
    "Staff" integer NOT NULL,
    "IsActive" boolean
);


ALTER TABLE "StaffSession" OWNER TO postgres;

--
-- TOC entry 2022 (class 2606 OID 16411)
-- Name: Client Client_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Client"
    ADD CONSTRAINT "Client_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2018 (class 2606 OID 16401)
-- Name: ConsultationType ConsultationType_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "ConsultationType"
    ADD CONSTRAINT "ConsultationType_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2030 (class 2606 OID 16435)
-- Name: SessionsBooked SessionsBooked_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "SessionsBooked"
    ADD CONSTRAINT "SessionsBooked_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2024 (class 2606 OID 16416)
-- Name: StaffSession StaffSession_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "StaffSession"
    ADD CONSTRAINT "StaffSession_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2020 (class 2606 OID 16406)
-- Name: Staff Staff_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Staff"
    ADD CONSTRAINT "Staff_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2032 (class 2606 OID 16437)
-- Name: SessionsBooked UQ_SessionsBooked_Client; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "SessionsBooked"
    ADD CONSTRAINT "UQ_SessionsBooked_Client" UNIQUE ("Client");


--
-- TOC entry 2034 (class 2606 OID 16439)
-- Name: SessionsBooked UQ_SessionsBooked_StaffSession; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "SessionsBooked"
    ADD CONSTRAINT "UQ_SessionsBooked_StaffSession" UNIQUE ("StaffSession");


--
-- TOC entry 2026 (class 2606 OID 16420)
-- Name: StaffSession UQ_StaffSession_ConsultationType; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "StaffSession"
    ADD CONSTRAINT "UQ_StaffSession_ConsultationType" UNIQUE ("ConsultationType");


--
-- TOC entry 2028 (class 2606 OID 16418)
-- Name: StaffSession UQ_StaffSession_Staff; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "StaffSession"
    ADD CONSTRAINT "UQ_StaffSession_Staff" UNIQUE ("Staff");


--
-- TOC entry 2037 (class 2606 OID 16445)
-- Name: Client FK_Client_SessionsBooked; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Client"
    ADD CONSTRAINT "FK_Client_SessionsBooked" FOREIGN KEY ("Id") REFERENCES "SessionsBooked"("Client");


--
-- TOC entry 2035 (class 2606 OID 16426)
-- Name: ConsultationType FK_ConsultationType_StaffSession; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "ConsultationType"
    ADD CONSTRAINT "FK_ConsultationType_StaffSession" FOREIGN KEY ("Id") REFERENCES "StaffSession"("ConsultationType");


--
-- TOC entry 2038 (class 2606 OID 16440)
-- Name: StaffSession FK_StaffSession_SessionsBooked; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "StaffSession"
    ADD CONSTRAINT "FK_StaffSession_SessionsBooked" FOREIGN KEY ("Id") REFERENCES "SessionsBooked"("StaffSession");


--
-- TOC entry 2036 (class 2606 OID 16421)
-- Name: Staff FK_Staff_StaffSession; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Staff"
    ADD CONSTRAINT "FK_Staff_StaffSession" FOREIGN KEY ("Id") REFERENCES "StaffSession"("Staff");


-- Completed on 2017-10-03 11:17:34

--
-- PostgreSQL database dump complete
--

