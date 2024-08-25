import { Task } from "~/types/task";

interface TaskDetailProps {
  task: Task;
}

export default function TaskDetail({ task }: TaskDetailProps) {
  return (
    <div className="border p-6 rounded shadow">
      <h2 className="text-2xl font-bold mb-4">{task.title}</h2>
      <p className="text-gray-600 mb-4">{task.description}</p>
      <div className="flex justify-between items-center mb-4">
        <span
          className={`px-3 py-1 rounded text-sm ${
            task.status === "todo"
              ? "bg-red-200 text-red-800"
              : task.status === "in-progress"
              ? "bg-yellow-200 text-yellow-800"
              : "bg-green-200 text-green-800"
          }`}
        >
          {task.status}
        </span>
        {task.dueDate && (
          <span className="text-sm text-gray-500">
            Due: {new Date(task.dueDate).toLocaleDateString()}
          </span>
        )}
      </div>
      <button className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600">
        Edit Task
      </button>
    </div>
  );
}
