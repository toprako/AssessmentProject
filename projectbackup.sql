PGDMP         6            
    {            AssesmentProjectDb    15.4    15.4                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    33072    AssesmentProjectDb    DATABASE     �   CREATE DATABASE "AssesmentProjectDb" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Turkish_T�rkiye.1254';
 $   DROP DATABASE "AssesmentProjectDb";
                postgres    false            �            1259    33085    Communications    TABLE     �   CREATE TABLE public."Communications" (
    "Id" uuid NOT NULL,
    "InformationType" smallint NOT NULL,
    "InformationContent" text,
    "PersonId" uuid
);
 $   DROP TABLE public."Communications";
       public         heap    postgres    false            �            1259    33078    Persons    TABLE     �   CREATE TABLE public."Persons" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "SurName" text NOT NULL,
    "Company" text
);
    DROP TABLE public."Persons";
       public         heap    postgres    false            �            1259    33098    Reports    TABLE     �   CREATE TABLE public."Reports" (
    "Id" uuid NOT NULL,
    "RequestDate" timestamp with time zone,
    "ReportStatus" text,
    "ReportJson" text
);
    DROP TABLE public."Reports";
       public         heap    postgres    false            �            1259    33073    __EFMigrationsHistory    TABLE     �   CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);
 +   DROP TABLE public."__EFMigrationsHistory";
       public         heap    postgres    false            
          0    33085    Communications 
   TABLE DATA           e   COPY public."Communications" ("Id", "InformationType", "InformationContent", "PersonId") FROM stdin;
    public          postgres    false    216   M       	          0    33078    Persons 
   TABLE DATA           G   COPY public."Persons" ("Id", "Name", "SurName", "Company") FROM stdin;
    public          postgres    false    215   q                 0    33098    Reports 
   TABLE DATA           V   COPY public."Reports" ("Id", "RequestDate", "ReportStatus", "ReportJson") FROM stdin;
    public          postgres    false    217   �                 0    33073    __EFMigrationsHistory 
   TABLE DATA           R   COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
    public          postgres    false    214   �       v           2606    33091     Communications PK_Communications 
   CONSTRAINT     d   ALTER TABLE ONLY public."Communications"
    ADD CONSTRAINT "PK_Communications" PRIMARY KEY ("Id");
 N   ALTER TABLE ONLY public."Communications" DROP CONSTRAINT "PK_Communications";
       public            postgres    false    216            s           2606    33084    Persons PK_Persons 
   CONSTRAINT     V   ALTER TABLE ONLY public."Persons"
    ADD CONSTRAINT "PK_Persons" PRIMARY KEY ("Id");
 @   ALTER TABLE ONLY public."Persons" DROP CONSTRAINT "PK_Persons";
       public            postgres    false    215            x           2606    33104    Reports PK_Reports 
   CONSTRAINT     V   ALTER TABLE ONLY public."Reports"
    ADD CONSTRAINT "PK_Reports" PRIMARY KEY ("Id");
 @   ALTER TABLE ONLY public."Reports" DROP CONSTRAINT "PK_Reports";
       public            postgres    false    217            q           2606    33077 .   __EFMigrationsHistory PK___EFMigrationsHistory 
   CONSTRAINT     {   ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");
 \   ALTER TABLE ONLY public."__EFMigrationsHistory" DROP CONSTRAINT "PK___EFMigrationsHistory";
       public            postgres    false    214            t           1259    33097    IX_Communications_PersonId    INDEX     _   CREATE INDEX "IX_Communications_PersonId" ON public."Communications" USING btree ("PersonId");
 0   DROP INDEX public."IX_Communications_PersonId";
       public            postgres    false    216            y           2606    33092 1   Communications FK_Communications_Persons_PersonId    FK CONSTRAINT     �   ALTER TABLE ONLY public."Communications"
    ADD CONSTRAINT "FK_Communications_Persons_PersonId" FOREIGN KEY ("PersonId") REFERENCES public."Persons"("Id") ON DELETE CASCADE;
 _   ALTER TABLE ONLY public."Communications" DROP CONSTRAINT "FK_Communications_Persons_PersonId";
       public          postgres    false    3187    216    215            
     x��VK�,�\W݅�H��,���!)	x<��O�C�`��v��PH4Й� #�m�?�V�<�Q�v�����C���?����_�>R�Τ�"�s*����)Ϣ�z�F�V��BQ�ΈU�Rx~Ce�jv
�h�2M�q~�5f�穮��-+i:S���,���Z�y�����{����ڠj�]6E����Q�����{�k�EMo���P��u�)��1����@o��� k+�k�k���*���4U)�1Z%��tgf���1ʱ2��vt`$�t�����3�ӔWqN2V�qh6vX�+^9'�֏����Z�Z/��4�%@3ˠ�1;O�#\�����w��S�E$g�qL��,	á5��l�����nQZ�/�9�K��I�-T�"�}Җ���D�r��n�վ�h/�Mݩ4�N־�E��6ي��q�}P�
���.�l'�L����A�=T�8)��m�R*�?(ni)��/�Uz�w<�j�������>\���Q�]m�Yh���~:e��{L�KT�ѷPu��������J�{�d1��Ta��6#/�TF��Ag=��Ԕ�X9]��۷�A^���d��U�[�2��S���;O��W��YI��^_����������`&�Ս�w#��U�����>�ֺ�C��x�QiElfݧ���3�G�o����`J�
::QR���ܲM�/�"�k!����~��A[�<]U���j9G ��\Dͺ:�ʱo�z� �)�[�:���\�%��#c
B�Kn_DW7��MI�����t�A���7!ޑ�t<�$JE@o()�tAv�|��o��IG����O
�H-�>d��e)��P��$��v�2��	�D��3����nנ���Y΋�g<��Zz�QJ׋�%�KC���29k� ��@�_yG�.L���b�(�v�m�/o=���4�C;�T�����X��		y1�̧�P3�v�������DJ0��_�pVX�G��7������]�ԋ�{����ØjܛF�8]���@7�/��3��A�Y '`�7�κJ)�-$�n[�'��
x��Cx�P�1�JQ�h��]O��yZl� ��|��eV2R_L�ӷPѵ8�Xm�i@3\ 
�1*��]�tQ秧�D�}���\_c�U�Hc�='&;�������U��݋�D���ƍ���-Ln[J���§�c�]��&�F����&�����s�(M_y~�d	���O/�ع�N��)LW����F�������`���!q���N��k��^7�{���ӷP������?�e��      	   B  x�M���T1���b�8v��+�������8�bwA�2��4Tt���h|��b���y��9!w�f�{��i���m�|s���~��zv1���c@͈@��Z�����JOP>S�c��0���ܱ=c�"]Z���������������T�d2�Ҿ�g�V!6�^<&����z{�1 kiq� LKڀx=Ê%�Ť[ڮ>}���G~{}�Zb!�04�!O�-FX}�Z�����{}���?(i*�*�U��K`�9�L�����ߕ��z}�
��ʳpl�V��j�|®M�����y8����0�ݻn0-r�a�-�^�.KM\SXոD�I47%L<GQ/tv��9i�3 ���/��6�:�B4N�C�݁)�y�Q�@���������	����f���e��,Y��θŠ�U�s��d�u���ږ�ۘaO��E=�i��Z[�Z�|�лcm�6�2+Dp-ħyFM�H�Ջcۓ������v9�h�֩cZ�-U�%��&�e�2�Q밋�j�f��ǅ�D
Q���ȝL�#����Q��!R"y�(q-� '��3ʈ��]�գ��\\\�[T�         �   x��;N�0E�d�-�"���t���x���R&i@,�=�0�j$>��������������!���h�dRO:�Jr� pq!|�U+���[+/9V�4�<Q�o���3��CYfZǒ%�)��e]�q���R��v�&-��`;��#?��>�z�}�y��圡~˸.=�i���i���������N(θ�	\�s�¶�7�K��_��j����w�1�         E   x�320264064�017����L����,�4�3�34�2BH[���SR�R�J�K�rRaJc���� 8��     