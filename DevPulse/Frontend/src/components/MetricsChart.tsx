import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';
import { Metric } from '../types';

interface MetricsChartProps {
  data: Metric[];
  metric: 'commitCount' | 'pullRequestCount' | 'issueCount';
  title: string;
}

export const MetricsChart = ({ data, metric, title }: MetricsChartProps) => {
  const chartData = data.map((m) => ({
    date: new Date(m.recordedAt).toLocaleDateString(),
    value: m[metric],
  }));

  return (
    <div className="w-full h-64">
      <h3 className="text-lg font-semibold mb-4">{title}</h3>
      <ResponsiveContainer width="100%" height="100%">
        <LineChart data={chartData}>
          <CartesianGrid strokeDasharray="3 3" />
          <XAxis dataKey="date" />
          <YAxis />
          <Tooltip />
          <Legend />
          <Line type="monotone" dataKey="value" stroke="#8884d8" />
        </LineChart>
      </ResponsiveContainer>
    </div>
  );
};
