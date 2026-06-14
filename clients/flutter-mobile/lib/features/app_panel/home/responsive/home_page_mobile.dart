part of '../home_page.dart';

class HomePageMobile extends StatefulWidget {
  const HomePageMobile({super.key});

  @override
  State<HomePageMobile> createState() => _HomePageMobileState();
}

class _HomePageMobileState extends State<HomePageMobile> {
  final rdm = Random();
  List<Map<String, dynamic>> streamData = [];
  late Timer timer;

  @override
  void dispose() {
    streamData.clear();
    timer.cancel();
    super.dispose();
  }

  @override
  void initState() {
    streamData = List.generate(3, (_) => {
      'x_axis': 'No.${rdm.nextInt(300)}',
      'y_axis': rdm.nextInt(300),
    });
    timer = Timer.periodic(const Duration(seconds: 2), (_) {
      if (mounted) {
        setState(() {
          streamData = List.generate(3, (_) => {
            'x_axis': 'No.${rdm.nextInt(300)}',
            'y_axis': rdm.nextInt(300),
          });
        });
      }
    });
    super.initState();
  }

  @override
  Widget build(BuildContext context) {
    return buildBaseScaffold(
      context,
      CustomScrollView(
        slivers: [
          SliverToBoxAdapter(
            child: Padding(
              padding: context.defaultPadding,
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  _buildGreeting(),
                  const SizedBox(height: 20),
                  _buildMembershipCard(context),
                  const SizedBox(height: 24),
                  _buildSectionTitle('Genel Bakış'),
                  const SizedBox(height: 12),
                  _buildStatsGrid(context),
                  const SizedBox(height: 24),
                  _buildSectionTitle('Bugünkü Dersler'),
                  const SizedBox(height: 12),
                  _buildUpcomingLessons(),
                  const SizedBox(height: 24),
                  _buildSectionTitle('Gelir Analizi'),
                  const SizedBox(height: 12),
                  SizedBox(
                    height: 260,
                    child: CoreInitializer().coreContainer.basicChart.getBarChart(
                      context,
                      basicData,
                      CoreScreenTexts.categories,
                      CoreScreenTexts.sales,
                      headerTitle: 'Aylık Gelir',
                      CustomColors.primary.getColor,
                    ),
                  ),
                  const SizedBox(height: 16),
                  SizedBox(
                    height: 260,
                    child: CoreInitializer().coreContainer.basicChart.getLineAreaChart(
                      context,
                      basicData,
                      CoreScreenTexts.categories,
                      CoreScreenTexts.sales,
                      headerTitle: 'Üye Artışı',
                      CustomColors.secondary.getColor,
                    ),
                  ),
                  const SizedBox(height: 16),
                  SizedBox(
                    height: 260,
                    child: CoreInitializer().coreContainer.basicChart.getPieChart(
                      context,
                      basicData,
                      headerTitle: 'Paket Dağılımı',
                      CoreScreenTexts.categories,
                      CoreScreenTexts.sales,
                    ),
                  ),
                  const SizedBox(height: 32),
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }

  Widget _buildGreeting() {
    final hour = DateTime.now().hour;
    final greeting = hour < 12 ? 'Günaydın' : hour < 18 ? 'İyi günler' : 'İyi akşamlar';
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        Text(
          greeting,
          style: TextStyle(
            fontSize: 13,
            color: CustomColors.gray.getColor,
            fontWeight: FontWeight.w500,
          ),
        ),
        const SizedBox(height: 2),
        const Text(
          'Fabrika Gym',
          style: TextStyle(fontSize: 24, fontWeight: FontWeight.bold),
        ),
      ],
    );
  }

  Widget _buildMembershipCard(BuildContext context) {
    return Container(
      width: double.infinity,
      padding: const EdgeInsets.all(20),
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(20),
        gradient: const LinearGradient(
          begin: Alignment.topLeft,
          end: Alignment.bottomRight,
          colors: [Color(0xFFFF5722), Color(0xFFFF8A65)],
        ),
        boxShadow: [
          BoxShadow(
            color: const Color(0xFFFF5722).withOpacity(0.35),
            blurRadius: 20,
            offset: const Offset(0, 6),
          ),
        ],
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              const Text(
                'Üyelik Kartı',
                style: TextStyle(color: Colors.white70, fontSize: 12, letterSpacing: 1),
              ),
              Container(
                padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 4),
                decoration: BoxDecoration(
                  color: Colors.white.withOpacity(0.2),
                  borderRadius: BorderRadius.circular(20),
                ),
                child: const Text(
                  'AKTİF',
                  style: TextStyle(color: Colors.white, fontSize: 11, fontWeight: FontWeight.bold),
                ),
              ),
            ],
          ),
          const SizedBox(height: 16),
          const Text(
            'Admin Kullanıcı',
            style: TextStyle(color: Colors.white, fontSize: 18, fontWeight: FontWeight.bold),
          ),
          const SizedBox(height: 4),
          const Text(
            'Yıllık Paket • 245 gün kaldı',
            style: TextStyle(color: Colors.white70, fontSize: 13),
          ),
          const SizedBox(height: 16),
          ClipRRect(
            borderRadius: BorderRadius.circular(4),
            child: LinearProgressIndicator(
              value: 0.33,
              backgroundColor: Colors.white24,
              valueColor: const AlwaysStoppedAnimation<Color>(Colors.white),
              minHeight: 6,
            ),
          ),
          const SizedBox(height: 6),
          const Text(
            '120/365 gün kullanıldı',
            style: TextStyle(color: Colors.white70, fontSize: 11),
          ),
        ],
      ),
    );
  }

  Widget _buildSectionTitle(String title) {
    return Text(
      title,
      style: const TextStyle(fontSize: 16, fontWeight: FontWeight.bold),
    );
  }

  Widget _buildStatsGrid(BuildContext context) {
    return GridView.count(
      crossAxisCount: 2,
      shrinkWrap: true,
      physics: const NeverScrollableScrollPhysics(),
      crossAxisSpacing: 12,
      mainAxisSpacing: 12,
      childAspectRatio: 1.6,
      children: [
        buildActiveMembersCard(context),
        buildDailyCheckInsCard(context),
        buildMonthlyRevenueCard(context),
        buildActiveLessonsCard(context),
      ],
    );
  }

  Widget _buildUpcomingLessons() {
    final lessons = [
      {'title': 'Sabah Fitness', 'time': '08:00', 'trainer': 'Ahmet Y.', 'capacity': '12/15', 'color': const Color(0xFFFF5722)},
      {'title': 'Yoga Başlangıç', 'time': '10:00', 'trainer': 'Zeynep K.', 'capacity': '8/10', 'color': const Color(0xFF1565C0)},
      {'title': 'Kickboks', 'time': '18:00', 'trainer': 'Murat D.', 'capacity': '6/12', 'color': const Color(0xFF2E7D32)},
    ];

    return Column(
      children: lessons.map((lesson) => _buildLessonTile(lesson)).toList(),
    );
  }

  Widget _buildLessonTile(Map<String, dynamic> lesson) {
    return Container(
      margin: const EdgeInsets.only(bottom: 10),
      padding: const EdgeInsets.all(14),
      decoration: BoxDecoration(
        borderRadius: BorderRadius.circular(14),
        color: Theme.of(context).cardColor,
        border: Border.all(color: Colors.grey.withOpacity(0.15)),
      ),
      child: Row(
        children: [
          Container(
            width: 4,
            height: 44,
            decoration: BoxDecoration(
              color: lesson['color'] as Color,
              borderRadius: BorderRadius.circular(2),
            ),
          ),
          const SizedBox(width: 12),
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                Text(
                  lesson['title'] as String,
                  style: const TextStyle(fontWeight: FontWeight.w600, fontSize: 14),
                ),
                const SizedBox(height: 3),
                Text(
                  '${lesson['trainer']} • ${lesson['capacity']} kişi',
                  style: TextStyle(fontSize: 12, color: CustomColors.gray.getColor),
                ),
              ],
            ),
          ),
          Column(
            crossAxisAlignment: CrossAxisAlignment.end,
            children: [
              Text(
                lesson['time'] as String,
                style: TextStyle(
                  fontWeight: FontWeight.bold,
                  fontSize: 15,
                  color: lesson['color'] as Color,
                ),
              ),
            ],
          ),
        ],
      ),
    );
  }
}
