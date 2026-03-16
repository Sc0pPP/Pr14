using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using pr14Avalonia.Models;

namespace UnitTest
{

    /// <summary>
    /// Класс тестов для модуля авторизации (EntrancePage).
    /// Метод Auth(login, password) возвращает true при успешном входе
    /// и false при любой ошибке (пустые поля, неверный логин/пароль).
    /// </summary>
    [TestClass]
    public class AuthTests
    {
        /// <summary>
        /// Тестовая база пользователей, имитирующая таблицу Users в БД.
        /// Содержит пользователей, которые реально есть в базе данных ПР14.
        /// </summary>
        private static readonly List<User> TestUsers = new List<User>
        {
            new User { Id = 1, Login = "admin",  Password = "admin"  },
            new User { Id = 2, Login = "user1",  Password = "1234"   },
            new User { Id = 3, Login = "petya",  Password = "qwerty" }
        };

        /// <summary>
        /// Имитация рефакторингового метода Auth(login, password).
        /// Логика: пустые поля -> false, логин не найден -> false,
        /// пароль не совпадает -> false, всё верно -> true.
        /// </summary>
        /// <param name="login">Логин пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>true — авторизация успешна, false — ошибка.</returns>
        private static bool Auth(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                return false;

            var user = TestUsers.FirstOrDefault(u => u.Login == login);
            if (user == null)
                return false;

            return user.Password == password;
        }

        // ══════════════════════════════════════════════════════════════
        //  ПУНКТ 1-2 ТЗ: AuthTest — базовый тест авторизации
        // ══════════════════════════════════════════════════════════════

        /// <summary>
        /// Базовый тест авторизации.
        /// Проверяет, что метод Auth возвращает true для верных данных
        /// и false для неверных.
        /// </summary>
        [TestMethod]
        public void AuthTest()
        {
            // Верный набор данных
            bool resultTrue = Auth("admin", "admin");

            // Неверный набор данных
            bool resultFalse = Auth("admin", "wrongpassword");

            Assert.IsTrue(resultTrue);
            Assert.IsFalse(resultFalse);
        }

        /// <summary>
        /// Позитивные тесты авторизации.
        /// Все пользователи из базы данных должны успешно войти
        /// с правильными логином и паролем.
        /// </summary>
        [TestMethod]
        public void AuthTestSuccess()
        {
            // --- Пользователь 1: admin ---
            bool result1 = Auth("admin", "admin");
            Assert.IsTrue(result1, "Пользователь 'admin' должен успешно авторизоваться.");

            // --- Пользователь 2: user1 ---
            bool result2 = Auth("user1", "1234");
            Assert.IsTrue(result2, "Пользователь 'user1' должен успешно авторизоваться.");

            // --- Пользователь 3: petya ---
            bool result3 = Auth("petya", "qwerty");
            Assert.IsTrue(result3, "Пользователь 'petya' должен успешно авторизоваться.");
        }

        /// <summary>
        /// Негативные тесты авторизации.
        /// Во всех перечисленных случаях вход должен быть отклонён (false).
        /// </summary>
        [TestMethod]
        public void AuthTestFail()
        {
            // --- Пустой логин ---
            bool emptyLogin = Auth("", "admin");
            Assert.IsFalse(emptyLogin, "Пустой логин — вход должен быть запрещён.");

            // --- Пустой пароль ---
            bool emptyPassword = Auth("admin", "");
            Assert.IsFalse(emptyPassword, "Пустой пароль — вход должен быть запрещён.");

            // --- Оба поля пусты ---
            bool bothEmpty = Auth("", "");
            Assert.IsFalse(bothEmpty, "Оба поля пусты — вход должен быть запрещён.");

            // --- Пробелы вместо логина ---
            bool whitespaceLogin = Auth("   ", "admin");
            Assert.IsFalse(whitespaceLogin, "Логин из пробелов — вход должен быть запрещён.");

            // --- Пробелы вместо пароля ---
            bool whitespacePassword = Auth("admin", "   ");
            Assert.IsFalse(whitespacePassword, "Пароль из пробелов — вход должен быть запрещён.");

            // --- Несуществующий логин ---
            bool unknownLogin = Auth("nosuchuser", "admin");
            Assert.IsFalse(unknownLogin, "Несуществующий логин — вход должен быть запрещён.");

            // --- Верный логин, неверный пароль ---
            bool wrongPassword = Auth("admin", "wrongpassword");
            Assert.IsFalse(wrongPassword, "Неверный пароль — вход должен быть запрещён.");

            // --- Верный логин, пароль в неверном регистре ---
            bool wrongCase = Auth("admin", "ADMIN");
            Assert.IsFalse(wrongCase, "Пароль с неверным регистром — вход должен быть запрещён.");

            // --- Несуществующий логин и неверный пароль ---
            bool bothWrong = Auth("ghost", "ghost");
            Assert.IsFalse(bothWrong, "Несуществующие данные — вход должен быть запрещён.");
        }
    }

    /// <summary>
    /// Класс тестов для модуля регистрации (RegistrationPage).
    /// Метод Registration(login, password, confirm) возвращает true при
    /// успешной регистрации и false при любой ошибке.
    /// </summary>
    [TestClass]
    public class RegistrationTests
    {
        /// <summary>
        /// Тестовая база пользователей, имитирующая текущее состояние таблицы Users.
        /// Используется для проверки уникальности логина при регистрации.
        /// </summary>
        private static readonly List<User> TestUsers = new List<User>
        {
            new User { Id = 1, Login = "admin", Password = "admin" },
            new User { Id = 2, Login = "user1", Password = "1234"  }
        };

        /// <summary>
        /// Имитация рефакторингового метода Registration(login, password, confirm).
        /// Логика: пустые поля -> false, логин занят -> false,
        /// пароли не совпадают -> false, всё верно -> true.
        /// </summary>
        /// <param name="login">Логин нового пользователя.</param>
        /// <param name="password">Пароль нового пользователя.</param>
        /// <param name="confirm">Подтверждение пароля.</param>
        /// <returns>true — регистрация успешна, false — ошибка.</returns>
        private static bool Registration(string login, string password, string confirm)
        {
            if (string.IsNullOrWhiteSpace(login) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirm))
                return false;

            if (TestUsers.Any(u => u.Login == login))
                return false;

            if (password != confirm)
                return false;

            return true;
        }
        /// <summary>
        /// Позитивные тесты регистрации.
        /// Пользователи с уникальными логинами и совпадающими паролями
        /// должны успешно регистрироваться.
        /// </summary>
        [TestMethod]
        public void RegistrationTestSuccess()
        {
            // --- Новый пользователь с корректными данными ---
            bool result1 = Registration("vasya", "pass123", "pass123");
            Assert.IsTrue(result1, "Уникальный логин и совпадающие пароли — регистрация должна пройти.");

            // --- Другой новый пользователь ---
            bool result2 = Registration("masha", "qwerty", "qwerty");
            Assert.IsTrue(result2, "Уникальный логин и совпадающие пароли — регистрация должна пройти.");

            // --- Логин с цифрами ---
            bool result3 = Registration("user99", "abc", "abc");
            Assert.IsTrue(result3, "Логин с цифрами и совпадающие пароли — регистрация должна пройти.");
        }
        /// <summary>
        /// Негативные тесты регистрации.
        /// Во всех перечисленных случаях регистрация должна быть отклонена (false).
        /// </summary>
        [TestMethod]
        public void RegistrationTestFail()
        {
            // --- Пустой логин ---
            bool emptyLogin = Registration("", "pass", "pass");
            Assert.IsFalse(emptyLogin, "Пустой логин — регистрация должна быть запрещена.");

            // --- Пустой пароль ---
            bool emptyPassword = Registration("newuser", "", "pass");
            Assert.IsFalse(emptyPassword, "Пустой пароль — регистрация должна быть запрещена.");

            // --- Пустое подтверждение пароля ---
            bool emptyConfirm = Registration("newuser", "pass", "");
            Assert.IsFalse(emptyConfirm, "Пустое подтверждение — регистрация должна быть запрещена.");

            // --- Все поля пусты ---
            bool allEmpty = Registration("", "", "");
            Assert.IsFalse(allEmpty, "Все поля пусты — регистрация должна быть запрещена.");

            // --- Пробелы вместо логина ---
            bool whitespaceLogin = Registration("   ", "pass", "pass");
            Assert.IsFalse(whitespaceLogin, "Логин из пробелов — регистрация должна быть запрещена.");

            // --- Логин уже занят (есть в БД) ---
            bool takenLogin1 = Registration("admin", "newpass", "newpass");
            Assert.IsFalse(takenLogin1, "Занятый логин 'admin' — регистрация должна быть запрещена.");

            // --- Другой занятый логин ---
            bool takenLogin2 = Registration("user1", "pass", "pass");
            Assert.IsFalse(takenLogin2, "Занятый логин 'user1' — регистрация должна быть запрещена.");

            // --- Пароли не совпадают ---
            bool mismatch = Registration("uniqueuser", "pass1", "pass2");
            Assert.IsFalse(mismatch, "Несовпадающие пароли — регистрация должна быть запрещена.");

            // --- Занятый логин + несовпадающие пароли ---
            bool takenAndMismatch = Registration("admin", "abc", "xyz");
            Assert.IsFalse(takenAndMismatch, "Занятый логин + несовпадающие пароли — регистрация должна быть запрещена.");
        }
    }
}