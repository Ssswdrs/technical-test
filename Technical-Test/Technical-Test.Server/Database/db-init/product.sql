
CREATE TABLE public.product (
    id integer NOT NULL,
    name character varying NOT NULL,
    sku character varying NOT NULL,
    price numeric(12,2) NOT NULL,
    stock integer NOT NULL,
    category_id integer
);


ALTER TABLE public.product OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 16418)
-- Name: product_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.product_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.product_id_seq OWNER TO postgres;

--
-- TOC entry 3421 (class 0 OID 0)
-- Dependencies: 215
-- Name: product_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.product_id_seq OWNED BY public.product.id;


--
-- TOC entry 3267 (class 2604 OID 16422)
-- Name: product id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.product ALTER COLUMN id SET DEFAULT nextval('public.product_id_seq'::regclass);


--
-- TOC entry 3415 (class 0 OID 16419)
-- Dependencies: 216
-- Data for Name: product; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.product VALUES (1, 'ข้าวผัด', 'FOOD001', 45.55, 20, 1) ON CONFLICT DO NOTHING;
INSERT INTO public.product VALUES (4, 'น้ำส้ม', 'FOOD002', 26.41, 49, 2) ON CONFLICT DO NOTHING;
INSERT INTO public.product VALUES (5, 'สบู่', 'FOOD003', 35.49, 0, 3) ON CONFLICT DO NOTHING;
INSERT INTO public.product VALUES (6, 'เสื้อยืด', 'FOOD004', 299.40, 5, 4) ON CONFLICT DO NOTHING;


--
-- TOC entry 3422 (class 0 OID 0)
-- Dependencies: 215
-- Name: product_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.product_id_seq', 9, true);


--
-- TOC entry 3269 (class 2606 OID 16424)
-- Name: product product_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.product
    ADD CONSTRAINT product_pkey PRIMARY KEY (id);


--
-- TOC entry 3270 (class 2606 OID 16434)
-- Name: product category_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.product
    ADD CONSTRAINT category_id FOREIGN KEY (category_id) REFERENCES public.category(id) NOT VALID;

