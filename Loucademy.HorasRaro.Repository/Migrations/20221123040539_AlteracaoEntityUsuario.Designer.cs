﻿// <auto-generated />
using System;
using Loucademy.HorasRaro.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Loucademy.HorasRaro.Repository.Migrations
{
    [DbContext(typeof(HorasRarasApiContext))]
    [Migration("20221123040539_AlteracaoEntityUsuario")]
    partial class AlteracaoEntityUsuario
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Loucademy.HorasRaro.Domain.Entities.CodigoConfirmacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataExpiracao")
                        .HasColumnType("datetime2");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int?>("usuarioAlteracao")
                        .HasColumnType("int");

                    b.Property<int?>("usuarioCriacao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UsuarioId");

                    b.ToTable("CodigosConfirmacao");
                });

            modelBuilder.Entity("Loucademy.HorasRaro.Domain.Entities.Projeto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("usuarioAlteracao")
                        .HasColumnType("int");

                    b.Property<int?>("usuarioCriacao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Projetos");
                });

            modelBuilder.Entity("Loucademy.HorasRaro.Domain.Entities.ProjetoUsuario", b =>
                {
                    b.Property<int>("ProjetoId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<int?>("usuarioAlteracao")
                        .HasColumnType("int");

                    b.Property<int?>("usuarioCriacao")
                        .HasColumnType("int");

                    b.HasKey("ProjetoId", "UsuarioId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("ProjetosUsuarios");
                });

            modelBuilder.Entity("Loucademy.HorasRaro.Domain.Entities.Tarefa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("HoraFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("HoraInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProjetoId")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.Property<int?>("usuarioAlteracao")
                        .HasColumnType("int");

                    b.Property<int?>("usuarioCriacao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjetoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Tarefas");
                });

            modelBuilder.Entity("Loucademy.HorasRaro.Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("Ativo")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("usuarioAlteracao")
                        .HasColumnType("int");

                    b.Property<int?>("usuarioCriacao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Loucademy.HorasRaro.Domain.Entities.CodigoConfirmacao", b =>
                {
                    b.HasOne("Loucademy.HorasRaro.Domain.Entities.Usuario", "Usuario")
                        .WithMany("CodigoConfirmacao")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Loucademy.HorasRaro.Domain.Entities.ProjetoUsuario", b =>
                {
                    b.HasOne("Loucademy.HorasRaro.Domain.Entities.Projeto", "Projeto")
                        .WithMany("ProjetoUsuario")
                        .HasForeignKey("ProjetoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Loucademy.HorasRaro.Domain.Entities.Usuario", "Usuario")
                        .WithMany("ProjetoUsuario")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Projeto");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Loucademy.HorasRaro.Domain.Entities.Tarefa", b =>
                {
                    b.HasOne("Loucademy.HorasRaro.Domain.Entities.Projeto", "Projeto")
                        .WithMany("Tarefas")
                        .HasForeignKey("ProjetoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Loucademy.HorasRaro.Domain.Entities.Usuario", "Usuario")
                        .WithMany("Tarefas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Projeto");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Loucademy.HorasRaro.Domain.Entities.Projeto", b =>
                {
                    b.Navigation("ProjetoUsuario");

                    b.Navigation("Tarefas");
                });

            modelBuilder.Entity("Loucademy.HorasRaro.Domain.Entities.Usuario", b =>
                {
                    b.Navigation("CodigoConfirmacao");

                    b.Navigation("ProjetoUsuario");

                    b.Navigation("Tarefas");
                });
#pragma warning restore 612, 618
        }
    }
}
