﻿// <auto-generated />
using System;
using LexiQuest.PuzzleMgr.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LexiQuest.PuzzleMgr.Infrastructure.Migrations
{
    [DbContext(typeof(PuzzleMgrDbContext))]
    [Migration("20240726150144_eboutbox")]
    partial class eboutbox
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LexiQuest.Framework.Application.Outbox.OutboxMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("OccurredOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ProcessedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OutboxMessages", "puzzlemgr");
                });

            modelBuilder.Entity("LexiQuest.Framework.Infrastructure.DataAccess.InternalCommands.InternalCommand", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("ProcessedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("InternalCommands", "puzzlemgr");
                });

            modelBuilder.Entity("LexiQuest.PuzzleMgr.Domain.PuzzleCollections.PuzzleCollection", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid[]>("_puzzles")
                        .IsRequired()
                        .HasColumnType("uuid[]")
                        .HasColumnName("Puzzles");

                    b.HasKey("Id");

                    b.ToTable("PuzzleCollection", "puzzlemgr");
                });

            modelBuilder.Entity("LexiQuest.PuzzleMgr.Domain.Puzzles.LanguageLevel", b =>
                {
                    b.Property<int>("Language")
                        .HasColumnType("integer");

                    b.Property<string>("TextRepresentation")
                        .HasColumnType("text");

                    b.HasKey("Language", "TextRepresentation");

                    b.ToTable("LanguageLevel", "puzzlemgr");
                });

            modelBuilder.Entity("LexiQuest.PuzzleMgr.Domain.Puzzles.Puzzle", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Language")
                        .HasColumnType("integer");

                    b.Property<int>("LevelLanguage")
                        .HasColumnType("integer");

                    b.Property<string>("LevelTextRepresentation")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PartsOfSpeech")
                        .HasColumnType("integer");

                    b.Property<string>("PuzzleOwnerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("LevelLanguage", "LevelTextRepresentation");

                    b.ToTable("Puzzles", "puzzlemgr");
                });

            modelBuilder.Entity("MassTransit.EntityFrameworkCoreIntegration.InboxState", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime?>("Consumed")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ConsumerId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("Delivered")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ExpirationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("LastSequenceNumber")
                        .HasColumnType("bigint");

                    b.Property<Guid>("LockId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uuid");

                    b.Property<int>("ReceiveCount")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Received")
                        .HasColumnType("timestamp with time zone");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.HasKey("Id");

                    b.HasAlternateKey("MessageId", "ConsumerId");

                    b.HasIndex("Delivered");

                    b.ToTable("EBInboxState", "puzzlemgr");
                });

            modelBuilder.Entity("MassTransit.EntityFrameworkCoreIntegration.OutboxMessage", b =>
                {
                    b.Property<long>("SequenceNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("SequenceNumber"));

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<Guid?>("ConversationId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CorrelationId")
                        .HasColumnType("uuid");

                    b.Property<string>("DestinationAddress")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime?>("EnqueueTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ExpirationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FaultAddress")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("Headers")
                        .HasColumnType("text");

                    b.Property<Guid?>("InboxConsumerId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("InboxMessageId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("InitiatorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uuid");

                    b.Property<string>("MessageType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("OutboxId")
                        .HasColumnType("uuid");

                    b.Property<string>("Properties")
                        .HasColumnType("text");

                    b.Property<Guid?>("RequestId")
                        .HasColumnType("uuid");

                    b.Property<string>("ResponseAddress")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<DateTime>("SentTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SourceAddress")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("SequenceNumber");

                    b.HasIndex("EnqueueTime");

                    b.HasIndex("ExpirationTime");

                    b.HasIndex("OutboxId", "SequenceNumber")
                        .IsUnique();

                    b.HasIndex("InboxMessageId", "InboxConsumerId", "SequenceNumber")
                        .IsUnique();

                    b.ToTable("EBOutboxMessage", "puzzlemgr");
                });

            modelBuilder.Entity("MassTransit.EntityFrameworkCoreIntegration.OutboxState", b =>
                {
                    b.Property<Guid>("OutboxId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("Delivered")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("LastSequenceNumber")
                        .HasColumnType("bigint");

                    b.Property<Guid>("LockId")
                        .HasColumnType("uuid");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("bytea");

                    b.HasKey("OutboxId");

                    b.HasIndex("Created");

                    b.ToTable("EBOutboxState", "puzzlemgr");
                });

            modelBuilder.Entity("LexiQuest.PuzzleMgr.Domain.PuzzleCollections.PuzzleCollection", b =>
                {
                    b.OwnsOne("LexiQuest.PuzzleMgr.Domain.PuzzleCollections.CollectionName", "Name", b1 =>
                        {
                            b1.Property<Guid>("PuzzleCollectionId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Name");

                            b1.HasKey("PuzzleCollectionId");

                            b1.ToTable("PuzzleCollection", "puzzlemgr");

                            b1.WithOwner()
                                .HasForeignKey("PuzzleCollectionId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("LexiQuest.PuzzleMgr.Domain.Puzzles.Puzzle", b =>
                {
                    b.HasOne("LexiQuest.PuzzleMgr.Domain.Puzzles.LanguageLevel", "Level")
                        .WithMany()
                        .HasForeignKey("LevelLanguage", "LevelTextRepresentation")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("LexiQuest.PuzzleMgr.Domain.Puzzles.Definition", "Definitions", b1 =>
                        {
                            b1.Property<Guid>("PuzzleId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Text");

                            b1.HasKey("PuzzleId", "Id");

                            b1.ToTable("Definitions", "puzzlemgr");

                            b1.WithOwner()
                                .HasForeignKey("PuzzleId");
                        });

                    b.OwnsMany("LexiQuest.PuzzleMgr.Domain.Puzzles.Example", "Examples", b1 =>
                        {
                            b1.Property<Guid>("PuzzleId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Text");

                            b1.HasKey("PuzzleId", "Id");

                            b1.ToTable("Examples", "puzzlemgr");

                            b1.WithOwner()
                                .HasForeignKey("PuzzleId");
                        });

                    b.OwnsOne("LexiQuest.PuzzleMgr.Domain.Puzzles.ForeignWords.ForeignWord", "ForeignWord", b1 =>
                        {
                            b1.Property<Guid>("PuzzleId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Language")
                                .HasColumnType("integer")
                                .HasColumnName("ForeignWordLanguage");

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("ForeignWordText");

                            b1.HasKey("PuzzleId");

                            b1.ToTable("Puzzles", "puzzlemgr");

                            b1.WithOwner()
                                .HasForeignKey("PuzzleId");
                        });

                    b.OwnsMany("LexiQuest.PuzzleMgr.Domain.Puzzles.ForeignWords.ForeignWord", "Synonims", b1 =>
                        {
                            b1.Property<Guid>("PuzzleId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Text");

                            b1.HasKey("PuzzleId", "Id");

                            b1.ToTable("Synonims", "puzzlemgr");

                            b1.WithOwner()
                                .HasForeignKey("PuzzleId");
                        });

                    b.OwnsOne("LexiQuest.PuzzleMgr.Domain.Puzzles.Transcription", "Transcription", b1 =>
                        {
                            b1.Property<Guid>("PuzzleId")
                                .HasColumnType("uuid");

                            b1.Property<string>("StrVal")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Transcription");

                            b1.HasKey("PuzzleId");

                            b1.ToTable("Puzzles", "puzzlemgr");

                            b1.WithOwner()
                                .HasForeignKey("PuzzleId");
                        });

                    b.Navigation("Definitions");

                    b.Navigation("Examples");

                    b.Navigation("ForeignWord")
                        .IsRequired();

                    b.Navigation("Level");

                    b.Navigation("Synonims");

                    b.Navigation("Transcription")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}