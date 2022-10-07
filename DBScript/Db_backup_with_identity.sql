--
-- PostgreSQL database dump
--

-- Dumped from database version 9.6.5
-- Dumped by pg_dump version 9.6.5

-- Started on 2017-10-03 16:09:41

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 2209 (class 1262 OID 16393)
-- Dependencies: 2208
-- Name: EAP_BMS; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON DATABASE "EAP_BMS" IS 'Booking Management System Database';


--
-- TOC entry 1 (class 3079 OID 12387)
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- TOC entry 2211 (class 0 OID 0)
-- Dependencies: 1
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


SET search_path = public, pg_catalog;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 190 (class 1259 OID 16453)
-- Name: AspNetRoles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "AspNetRoles" (
    "Id" character varying(128) NOT NULL,
    "Name" character varying(256) NOT NULL
);


ALTER TABLE "AspNetRoles" OWNER TO postgres;

--
-- TOC entry 193 (class 1259 OID 16474)
-- Name: AspNetUserClaims; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "AspNetUserClaims" (
    "Id" integer NOT NULL,
    "ClaimType" character varying(256),
    "ClaimValue" character varying(256),
    "UserId" character varying(128) NOT NULL
);


ALTER TABLE "AspNetUserClaims" OWNER TO postgres;

--
-- TOC entry 192 (class 1259 OID 16472)
-- Name: AspNetUserClaims_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE "AspNetUserClaims_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE "AspNetUserClaims_Id_seq" OWNER TO postgres;

--
-- TOC entry 2212 (class 0 OID 0)
-- Dependencies: 192
-- Name: AspNetUserClaims_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE "AspNetUserClaims_Id_seq" OWNED BY "AspNetUserClaims"."Id";


--
-- TOC entry 194 (class 1259 OID 16483)
-- Name: AspNetUserLogins; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "AspNetUserLogins" (
    "UserId" character varying(128) NOT NULL,
    "LoginProvider" character varying(128) NOT NULL,
    "ProviderKey" character varying(128) NOT NULL
);


ALTER TABLE "AspNetUserLogins" OWNER TO postgres;

--
-- TOC entry 195 (class 1259 OID 16488)
-- Name: AspNetUserRoles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "AspNetUserRoles" (
    "UserId" character varying(128) NOT NULL,
    "RoleId" character varying(128) NOT NULL
);


ALTER TABLE "AspNetUserRoles" OWNER TO postgres;

--
-- TOC entry 191 (class 1259 OID 16458)
-- Name: AspNetUsers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "AspNetUsers" (
    "Id" character varying(128) NOT NULL,
    "UserName" character varying(256) NOT NULL,
    "PasswordHash" character varying(256),
    "SecurityStamp" character varying(256),
    "Email" character varying(256) DEFAULT NULL::character varying,
    "EmailConfirmed" boolean DEFAULT false NOT NULL,
    "PhoneNumber" character varying(256),
    "PhoneNumberConfirmed" boolean DEFAULT false NOT NULL,
    "TwoFactorEnabled" boolean DEFAULT false NOT NULL,
    "LockoutEndDateUtc" timestamp without time zone,
    "LockoutEnabled" boolean DEFAULT false NOT NULL,
    "AccessFailedCount" integer DEFAULT 0 NOT NULL
);


ALTER TABLE "AspNetUsers" OWNER TO postgres;

--
-- TOC entry 187 (class 1259 OID 16407)
-- Name: Client; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE "Client" (
    "Id" integer NOT NULL,
    "Name" character varying(100) NOT NULL,
    "Email" character varying(100) NOT NULL,
    "Phone" numeric(12,0) NOT NULL,
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
    "Client" integer,
    "StaffSession" integer,
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
    "Phone" numeric(12,0) NOT NULL,
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
-- TOC entry 2046 (class 2604 OID 16477)
-- Name: AspNetUserClaims Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "AspNetUserClaims" ALTER COLUMN "Id" SET DEFAULT nextval('"AspNetUserClaims_Id_seq"'::regclass);


--
-- TOC entry 2066 (class 2606 OID 16457)
-- Name: AspNetRoles AspNetRoles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "AspNetRoles"
    ADD CONSTRAINT "AspNetRoles_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2070 (class 2606 OID 16482)
-- Name: AspNetUserClaims AspNetUserClaims_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "AspNetUserClaims"
    ADD CONSTRAINT "AspNetUserClaims_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2073 (class 2606 OID 16487)
-- Name: AspNetUserLogins AspNetUserLogins_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "AspNetUserLogins"
    ADD CONSTRAINT "AspNetUserLogins_pkey" PRIMARY KEY ("UserId", "LoginProvider", "ProviderKey");


--
-- TOC entry 2076 (class 2606 OID 16492)
-- Name: AspNetUserRoles AspNetUserRoles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "AspNetUserRoles"
    ADD CONSTRAINT "AspNetUserRoles_pkey" PRIMARY KEY ("UserId", "RoleId");


--
-- TOC entry 2068 (class 2606 OID 16471)
-- Name: AspNetUsers AspNetUsers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "AspNetUsers"
    ADD CONSTRAINT "AspNetUsers_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2052 (class 2606 OID 16411)
-- Name: Client Client_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Client"
    ADD CONSTRAINT "Client_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2048 (class 2606 OID 16401)
-- Name: ConsultationType ConsultationType_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "ConsultationType"
    ADD CONSTRAINT "ConsultationType_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2060 (class 2606 OID 16435)
-- Name: SessionsBooked SessionsBooked_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "SessionsBooked"
    ADD CONSTRAINT "SessionsBooked_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2054 (class 2606 OID 16416)
-- Name: StaffSession StaffSession_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "StaffSession"
    ADD CONSTRAINT "StaffSession_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2050 (class 2606 OID 16406)
-- Name: Staff Staff_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "Staff"
    ADD CONSTRAINT "Staff_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 2062 (class 2606 OID 16437)
-- Name: SessionsBooked UQ_SessionsBooked_Client; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "SessionsBooked"
    ADD CONSTRAINT "UQ_SessionsBooked_Client" UNIQUE ("Client");


--
-- TOC entry 2064 (class 2606 OID 16439)
-- Name: SessionsBooked UQ_SessionsBooked_StaffSession; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "SessionsBooked"
    ADD CONSTRAINT "UQ_SessionsBooked_StaffSession" UNIQUE ("StaffSession");


--
-- TOC entry 2056 (class 2606 OID 16420)
-- Name: StaffSession UQ_StaffSession_ConsultationType; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "StaffSession"
    ADD CONSTRAINT "UQ_StaffSession_ConsultationType" UNIQUE ("ConsultationType");


--
-- TOC entry 2058 (class 2606 OID 16418)
-- Name: StaffSession UQ_StaffSession_Staff; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "StaffSession"
    ADD CONSTRAINT "UQ_StaffSession_Staff" UNIQUE ("Staff");


--
-- TOC entry 2071 (class 1259 OID 16493)
-- Name: IX_AspNetUserClaims_UserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AspNetUserClaims_UserId" ON "AspNetUserClaims" USING btree ("UserId");


--
-- TOC entry 2074 (class 1259 OID 16494)
-- Name: IX_AspNetUserLogins_UserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AspNetUserLogins_UserId" ON "AspNetUserLogins" USING btree ("UserId");


--
-- TOC entry 2077 (class 1259 OID 16495)
-- Name: IX_AspNetUserRoles_RoleId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AspNetUserRoles_RoleId" ON "AspNetUserRoles" USING btree ("RoleId");


--
-- TOC entry 2078 (class 1259 OID 16496)
-- Name: IX_AspNetUserRoles_UserId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_AspNetUserRoles_UserId" ON "AspNetUserRoles" USING btree ("UserId");


--
-- TOC entry 2083 (class 2606 OID 16497)
-- Name: AspNetUserClaims FK_AspNetUserClaims_AspNetUsers_User_Id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "AspNetUserClaims"
    ADD CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_User_Id" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 2084 (class 2606 OID 16502)
-- Name: AspNetUserLogins FK_AspNetUserLogins_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "AspNetUserLogins"
    ADD CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 2085 (class 2606 OID 16507)
-- Name: AspNetUserRoles FK_AspNetUserRoles_AspNetRoles_RoleId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "AspNetUserRoles"
    ADD CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles"("Id") ON DELETE CASCADE;


--
-- TOC entry 2086 (class 2606 OID 16512)
-- Name: AspNetUserRoles FK_AspNetUserRoles_AspNetUsers_UserId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "AspNetUserRoles"
    ADD CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers"("Id") ON DELETE CASCADE;


--
-- TOC entry 2081 (class 2606 OID 16525)
-- Name: SessionsBooked FK_SessionsBooked_Client; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "SessionsBooked"
    ADD CONSTRAINT "FK_SessionsBooked_Client" FOREIGN KEY ("Client") REFERENCES "Client"("Id");


--
-- TOC entry 2082 (class 2606 OID 16530)
-- Name: SessionsBooked FK_SessionsBooked_StaffSession; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "SessionsBooked"
    ADD CONSTRAINT "FK_SessionsBooked_StaffSession" FOREIGN KEY ("StaffSession") REFERENCES "StaffSession"("Id");


--
-- TOC entry 2079 (class 2606 OID 16535)
-- Name: StaffSession FK_StaffSession_ConsultationType; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "StaffSession"
    ADD CONSTRAINT "FK_StaffSession_ConsultationType" FOREIGN KEY ("ConsultationType") REFERENCES "StaffSession"("Id");


--
-- TOC entry 2080 (class 2606 OID 16540)
-- Name: StaffSession FK_StaffSession_Staff; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY "StaffSession"
    ADD CONSTRAINT "FK_StaffSession_Staff" FOREIGN KEY ("Staff") REFERENCES "Staff"("Id");


-- Completed on 2017-10-03 16:09:42

--
-- PostgreSQL database dump complete
--

