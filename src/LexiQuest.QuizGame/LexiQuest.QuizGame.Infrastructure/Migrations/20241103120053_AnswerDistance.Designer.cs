﻿// <auto-generated />
using System;
using System.Collections.Generic;
using LexiQuest.QuizGame.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LexiQuest.QuizGame.Infrastructure.Migrations
{
    [DbContext(typeof(QuizGameContext))]
    [Migration("20241103120053_AnswerDistance")]
    partial class AnswerDistance
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LexiQuest.Framework.Application.Messages.InternalProcessing.OutboxMessage", b =>
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

                    b.ToTable("_OutboxMessages", "quizgame");
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

                    b.ToTable("_InternalCommands", "quizgame");
                });

            modelBuilder.Entity("LexiQuest.QuizGame.Application.Handlers.StartNewGameSaga.StartNewGameSagaState", b =>
                {
                    b.Property<Guid>("CorrelationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CheckLimitRequestId")
                        .HasColumnType("uuid");

                    b.Property<string>("CurrentState")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("GameCreated")
                        .HasColumnType("boolean");

                    b.Property<bool>("GameStarted")
                        .HasColumnType("boolean");

                    b.Property<bool>("Initialized")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("NewGameId")
                        .HasColumnType("uuid");

                    b.Property<string>("PlayerId")
                        .HasColumnType("text");

                    b.Property<Guid?>("PuzzleRequestId")
                        .HasColumnType("uuid");

                    b.Property<bool>("PuzzlesFetched")
                        .HasColumnType("boolean");

                    b.Property<bool>("RefusedAlreadyStarted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("StartNewGameId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CorrelationId");

                    b.ToTable("StartNewGameSagaState", "quizgame");
                });

            modelBuilder.Entity("LexiQuest.QuizGame.Domain.Decks.CardDeck", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("PlayerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("CardDecks", "quizgame");
                });

            modelBuilder.Entity("LexiQuest.QuizGame.Domain.Decks.FaceDownCard", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("DeckId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DeckId");

                    b.ToTable("FaceDownCards", "quizgame");
                });

            modelBuilder.Entity("LexiQuest.QuizGame.Domain.FaceUpCards.FaceUpCard", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<double>("AnswerDistance")
                        .HasColumnType("double precision");

                    b.Property<DateTimeOffset?>("CompletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uuid");

                    b.Property<string>("Hint")
                        .HasColumnType("text");

                    b.Property<int?>("LastResult")
                        .HasColumnType("integer");

                    b.Property<bool>("Mistaken")
                        .HasColumnType("boolean");

                    b.Property<string>("PlayerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FaceUpCards", "quizgame");
                });

            modelBuilder.Entity("LexiQuest.QuizGame.Domain.GameStates.GameState", b =>
                {
                    b.Property<Guid>("GameId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CardDeckId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("CreatedTimestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PlayerId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("GameId");

                    b.ToTable("GameStates", "quizgame");
                });

            modelBuilder.Entity("LexiQuest.QuizGame.Domain.Players.Player", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<Guid[]>("_games")
                        .IsRequired()
                        .HasColumnType("uuid[]")
                        .HasColumnName("Games");

                    b.HasKey("Id");

                    b.ToTable("Player", "quizgame");
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

                    b.ToTable("_MT_InboxState", "quizgame");
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

                    b.ToTable("_MT_OutboxMessage", "quizgame");
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

                    b.ToTable("_MT_OutboxState", "quizgame");
                });

            modelBuilder.Entity("LexiQuest.QuizGame.Domain.Decks.FaceDownCard", b =>
                {
                    b.HasOne("LexiQuest.QuizGame.Domain.Decks.CardDeck", null)
                        .WithMany("Cards")
                        .HasForeignKey("DeckId");

                    b.OwnsOne("LexiQuest.QuizGame.Domain.Decks.FaceDownCardPuzzleInfo", "FaceDownCardPuzzleInfo", b1 =>
                        {
                            b1.Property<Guid>("FaceDownCardId")
                                .HasColumnType("uuid");

                            b1.Property<string>("ForeignWord")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("ForeignWord");

                            b1.Property<string>("From")
                                .HasColumnType("text")
                                .HasColumnName("From");

                            b1.Property<int>("Language")
                                .HasColumnType("integer")
                                .HasColumnName("Language");

                            b1.Property<string>("Level")
                                .HasColumnType("text")
                                .HasColumnName("Level");

                            b1.Property<string>("PartsOfSpeech")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("PartsOfSpeech");

                            b1.Property<string>("Transcription")
                                .HasColumnType("text")
                                .HasColumnName("Transcription");

                            b1.Property<List<string>>("_definitions")
                                .IsRequired()
                                .HasColumnType("text[]")
                                .HasColumnName("Definitions");

                            b1.Property<List<string>>("_examples")
                                .IsRequired()
                                .HasColumnType("text[]")
                                .HasColumnName("Examples");

                            b1.Property<List<string>>("_synonims")
                                .IsRequired()
                                .HasColumnType("text[]")
                                .HasColumnName("Synonims");

                            b1.HasKey("FaceDownCardId");

                            b1.ToTable("FaceDownCards", "quizgame");

                            b1.WithOwner()
                                .HasForeignKey("FaceDownCardId");
                        });

                    b.Navigation("FaceDownCardPuzzleInfo")
                        .IsRequired();
                });

            modelBuilder.Entity("LexiQuest.QuizGame.Domain.FaceUpCards.FaceUpCard", b =>
                {
                    b.OwnsOne("LexiQuest.QuizGame.Domain.FaceUpCards.FaceUpCardPuzzleInfo", "PuzzleInfo", b1 =>
                        {
                            b1.Property<Guid>("FaceUpCardId")
                                .HasColumnType("uuid");

                            b1.Property<Guid>("FaceDownCardId")
                                .HasColumnType("uuid");

                            b1.Property<string>("ForeignWord")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("ForeignWord");

                            b1.Property<string>("From")
                                .HasColumnType("text")
                                .HasColumnName("From");

                            b1.Property<int>("Language")
                                .HasColumnType("integer")
                                .HasColumnName("Language");

                            b1.Property<string>("Level")
                                .HasColumnType("text")
                                .HasColumnName("Level");

                            b1.Property<string>("PartsOfSpeech")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("PartsOfSpeech");

                            b1.Property<string>("Transcription")
                                .HasColumnType("text")
                                .HasColumnName("Transcription");

                            b1.Property<List<string>>("_definitions")
                                .IsRequired()
                                .HasColumnType("text[]")
                                .HasColumnName("Definitions");

                            b1.Property<List<string>>("_examples")
                                .IsRequired()
                                .HasColumnType("text[]")
                                .HasColumnName("Examples");

                            b1.Property<List<string>>("_synonims")
                                .IsRequired()
                                .HasColumnType("text[]")
                                .HasColumnName("Synonims");

                            b1.HasKey("FaceUpCardId");

                            b1.ToTable("FaceUpCards", "quizgame");

                            b1.WithOwner()
                                .HasForeignKey("FaceUpCardId");
                        });

                    b.Navigation("PuzzleInfo")
                        .IsRequired();
                });

            modelBuilder.Entity("LexiQuest.QuizGame.Domain.GameStates.GameState", b =>
                {
                    b.OwnsOne("LexiQuest.QuizGame.Domain.GameStates.Score", "Score", b1 =>
                        {
                            b1.Property<Guid>("GameStateGameId")
                                .HasColumnType("uuid");

                            b1.Property<int>("IntVal")
                                .HasColumnType("integer")
                                .HasColumnName("Score");

                            b1.HasKey("GameStateGameId");

                            b1.ToTable("GameStates", "quizgame");

                            b1.WithOwner()
                                .HasForeignKey("GameStateGameId");
                        });

                    b.Navigation("Score")
                        .IsRequired();
                });

            modelBuilder.Entity("LexiQuest.QuizGame.Domain.Decks.CardDeck", b =>
                {
                    b.Navigation("Cards");
                });
#pragma warning restore 612, 618
        }
    }
}
