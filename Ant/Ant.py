import numpy as ny
import random
import matplotlib.pyplot as plt
from numba import jit

''' 蚁群算法求解TSP问题'''


# 蚁群优化算法
class AntOptimization:
    # 城市个数
    __city_count = 0
    # 城市集合
    __city_set = 0
    # 蚂蚁个数
    __ant_count = 0
    # 城市距离矩阵
    __city_distance = 0
    # 信息素矩阵
    __city_message = 0
    # 蚂蚁路径集合
    __ants = 0
    # 最优蚂蚁路径
    __bast_ant = 0
    # 停滞计数器
    __loop_count = 0
    # 最大停滞次数
    __max_loop_times = 100
    # 信息素最小值
    __min_message = 0.01
    # 信息素最大值
    __max_message = 100
    # 蚂蚁倍数
    __ant_power = 0.5
    # 选择信息素权重
    __message_weight = 1
    # 选择城市距离权重
    __distance_weight = 1

    # 初始化
    def __init__(self, city_count=30):
        _data = [(41, 94), (37, 84), (54, 67), (25, 62), (7, 64),
                 (2, 99), (68, 58), (71, 44), (54, 62), (83, 69),
                 (64, 60), (18, 54), (22, 60), (83, 46), (91, 38),
                 (25, 38), (24, 42), (58, 69), (71, 71), (74, 78),
                 (87, 76), (18, 40), (13, 40), (82, 7), (62, 32),
                 (58, 35), (45, 21), (41, 26), (44, 35), (4, 50)
                 ]
        if city_count == 30:
            self.__city_set = ny.asarray(_data).T
        else:
            self.__city_set = ny.random.randint(1, 50, [2, city_count])
        print(self.__city_set)
        self.__city_count = city_count
        self.__ant_count = int(city_count * self.__ant_power)
        self.__city_distance = ny.zeros([self.__city_count, self.__city_count], dtype=float) * 10
        self.cal_distance()
        self.__city_message = ny.ones([self.__city_count, self.__city_count], dtype=float) * self.__min_message
        self.__ants = ny.zeros([self.__ant_count, self.__city_count], dtype=int)
        self.__bast_ant = ny.arange(city_count)
        self.draw(save_times=-1, save_ant=-1)

    # 计算两座城市距离
    def cal_distance(self):
        for i in range(self.__city_count):
            for j in range(self.__city_count):
                a = self.__city_set[:, i]
                b = self.__city_set[:, j]
                x = a[0] - b[0]
                y = a[1] - b[1]
                self.__city_distance[i, j] = ny.sqrt(x ** 2 + y ** 2)

    # 向路径添加信息素
    def add_message(self, ant_way, k=1):
        way = list(ant_way)
        for i in range(self.__city_count - 1):
            now_city = way[i]
            next_city = way[i + 1]
            self.__city_message[now_city, next_city] += k / self.__city_distance[now_city, next_city]
            if self.__city_message[now_city, next_city] > self.__max_message:
                self.__city_message[now_city, next_city] = self.__max_message
            elif self.__city_message[now_city, next_city] < self.__min_message:
                self.__city_message[now_city, next_city] = self.__min_message
            self.__city_message[next_city, now_city] = self.__city_message[now_city, next_city]

    # 第ant_index只蚂蚁选择下一个城市
    # 访问次数_visit
    # 未访问城市集合city
    def ant_select_city(self, ant_index, _visit, city):
        # 获取当前城市now_city
        now_city = self.__ants[ant_index, _visit - 1]
        # 选择每个城市的概率p_city
        p_city = ny.zeros(len(city), dtype=float)
        for i in range(len(city)):
            city_index = city[i]
            # 遍历城市集计算概率
            p_city[i] = ny.power(self.__city_message[now_city, city_index], self.__message_weight) * ny.power(
                (1 / self.__city_distance[now_city, city_index]), self.__distance_weight)
        '''
        # 随机锦标赛法选择下一个城市
        a = random.randint(0, len(city) - 1)
        b = random.randint(0, len(city) - 1)
        if p_city[a] > p_city[b]:
            next_city = a
        else:
            next_city = b
        '''
        # 不加随机算法了
        # 直接选择最好的城市
        # (′д｀ )…彡…彡
        _city = list(p_city)
        next_city = _city.index(max(_city))
        next_city = city[next_city]
        self.__ants[ant_index, _visit] = next_city
        self.__city_message[now_city, next_city] += 1 / self.__city_distance[now_city, next_city]
        if self.__city_message[now_city, next_city] > self.__max_message:
            self.__city_message[now_city, next_city] = self.__max_message
        elif self.__city_message[now_city, next_city] < self.__min_message:
            self.__city_message[now_city, next_city] = self.__min_message
        self.__city_message[next_city, now_city] = self.__city_message[now_city, next_city]

    #  第ant_index只蚂蚁选择路径
    def ant_select(self, ant_index, _visit):
        if _visit == 0:
            # 第一次随机选择起点
            random_city = random.randint(0, self.__city_count - 1)
            self.__ants[ant_index, _visit] = random_city
        else:
            # 计算剩余城市集合city
            city = range(self.__city_count)
            city = [x for x in city if x not in self.__ants[ant_index, 1:_visit]]
            # city.remove(self.__ants[ant_index, 1:_visit])
            # 选取下一个城市
            self.ant_select_city(ant_index, _visit, city)

    # 计算路径长度
    def cal_length(self, _ants):
        _sum = 0
        _ants = list(_ants)
        _ants.append(_ants[0])
        for i in range(self.__city_count):
            now_city = _ants[i]
            next_city = _ants[i + 1]
            _sum += self.__city_distance[now_city, next_city]
        return _sum

    # 储存最优路径
    def save_ant(self, save_times=0):
        _bast = self.cal_length(self.__bast_ant)
        for i in range(self.__ant_count):
            _now = self.cal_length(self.__ants[i, :])
            if _now < _bast:
                self.__loop_count = 0
                _bast = _now
                self.__bast_ant = list(self.__ants[i, :])
                self.draw(save_times=save_times, save_ant=i)
            else:
                self.__loop_count += 1
        # 停滞次数到达阈值
        if self.__loop_count / self.__ant_count >= self.__max_loop_times:
            # 清空信息素
            print("clear")
            self.__city_message *= 0
            self.__loop_count = 0

    # 运行
    def run(self, times):
        for now_time in range(times + 1):
            # 信息素挥发
            self.__city_message *= 0.7
            # 初始化蚂蚁路径
            self.__ants *= 0
            for city_visit in range(self.__city_count):
                # 访问第city_visit个城市
                for i in range(self.__ant_count):
                    # 第i只蚂蚁选择路径
                    self.ant_select(i, city_visit)
            for i in range(self.__ant_count):
                self.add_message(self.__ants[1, :], 1)
            print(">>>{}".format(now_time + 1))
            self.save_ant(save_times=now_time)
            # self.add_message(self.__bast_ant, 1)

    # 绘制结果
    def draw(self, save_times=0, save_ant=0, show=False):
        save_times += 1
        save_ant += 1
        draw_point = self.__city_set[:, self.__bast_ant]
        x = list(draw_point[0, :])
        y = list(draw_point[1, :])
        x.append(x[0])
        y.append(y[0])
        plt.plot(x, y)
        plt.title(self.cal_length(self.__bast_ant).__str__())
        plt.savefig("image\\times_" + save_times.__str__() + "_" + save_ant.__str__())
        if show:
            plt.show()
        else:
            plt.close()


ant = AntOptimization(30)
ant.run(times=100)
