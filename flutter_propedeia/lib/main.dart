import 'dart:math';

import 'package:flutter/material.dart';

void main() {
  runApp(const PropedeiaApp());
}

class PropedeiaApp extends StatelessWidget {
  const PropedeiaApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      title: 'Προπαίδεια',
      theme: ThemeData(
        colorScheme: ColorScheme.fromSeed(seedColor: Colors.indigo),
        useMaterial3: true,
      ),
      home: const MultiplicationGamePage(),
    );
  }
}

class MultiplicationGamePage extends StatefulWidget {
  const MultiplicationGamePage({super.key});

  @override
  State<MultiplicationGamePage> createState() => _MultiplicationGamePageState();
}

class _MultiplicationGamePageState extends State<MultiplicationGamePage> {
  final Random _random = Random();
  final TextEditingController _answerController = TextEditingController();

  int _a = 1;
  int _b = 1;
  int _score = 0;
  int _total = 0;
  String _message = 'Πάτα "Έλεγχος" για να ξεκινήσεις!';

  @override
  void initState() {
    super.initState();
    _nextQuestion();
  }

  @override
  void dispose() {
    _answerController.dispose();
    super.dispose();
  }

  void _nextQuestion() {
    setState(() {
      _a = _random.nextInt(10) + 1;
      _b = _random.nextInt(10) + 1;
      _answerController.clear();
    });
  }

  void _checkAnswer() {
    final int? answer = int.tryParse(_answerController.text.trim());
    final int correct = _a * _b;

    setState(() {
      _total++;
      if (answer == correct) {
        _score++;
        _message = 'Μπράβο! Σωστό ✅';
      } else {
        _message = 'Λάθος. Το σωστό είναι $correct.';
      }
    });

    _nextQuestion();
  }

  void _resetScore() {
    setState(() {
      _score = 0;
      _total = 0;
      _message = 'Νέα αρχή!';
    });
    _nextQuestion();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Παιχνίδι Προπαίδειας')),
      body: Padding(
        padding: const EdgeInsets.all(20),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            Text(
              'Πόσο κάνει: $_a × $_b ;',
              style: Theme.of(context).textTheme.headlineMedium,
              textAlign: TextAlign.center,
            ),
            const SizedBox(height: 20),
            TextField(
              controller: _answerController,
              keyboardType: TextInputType.number,
              textInputAction: TextInputAction.done,
              onSubmitted: (_) => _checkAnswer(),
              decoration: const InputDecoration(
                border: OutlineInputBorder(),
                labelText: 'Γράψε την απάντηση',
              ),
            ),
            const SizedBox(height: 12),
            ElevatedButton(
              onPressed: _checkAnswer,
              child: const Text('Έλεγχος'),
            ),
            const SizedBox(height: 8),
            OutlinedButton(
              onPressed: _resetScore,
              child: const Text('Μηδενισμός Σκορ'),
            ),
            const SizedBox(height: 20),
            Text(
              _message,
              style: Theme.of(context).textTheme.titleMedium,
              textAlign: TextAlign.center,
            ),
            const Spacer(),
            Card(
              child: Padding(
                padding: const EdgeInsets.all(16),
                child: Text(
                  'Σκορ: $_score / $_total',
                  style: Theme.of(context).textTheme.headlineSmall,
                  textAlign: TextAlign.center,
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
